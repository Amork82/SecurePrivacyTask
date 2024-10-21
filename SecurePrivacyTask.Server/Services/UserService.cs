using MongoDB.Driver;
using SecurePrivacyTask.Server.DBContext;
using SecurePrivacyTask.Server.Dto.Input;
using SecurePrivacyTask.Server.Dto.Output;
using SecurePrivacyTask.Server.Models;

namespace SecurePrivacyTask.Server.Services
{
    public class UserService : BaseService
    {
        private readonly IMongoCollection<User>? _usersCollection;

        public UserService(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _usersCollection = _mongoDBContext.Database?.GetCollection<User>("users");
        }

        public async Task<List<UserDtoOutput>> GetUserList(UserFilterDto filter)
        {
            var filterBuilder = Builders<User>.Filter;
            var filters = new List<FilterDefinition<User>>();

            //Applied filter for first name
            if (!string.IsNullOrEmpty(filter.firstName))
            {
                filters.Add(filterBuilder.Regex(user => user.firstName, new MongoDB.Bson.BsonRegularExpression(filter.firstName, "i")));

            }

            //Applied filter for last name
            if (!string.IsNullOrEmpty(filter.lastName))
            {
                filters.Add(filterBuilder.Regex(user => user.lastName, new MongoDB.Bson.BsonRegularExpression(filter.lastName, "i")));
            }

            //Applied filter for date of birth
            if (filter.dateOfBirth.HasValue)
            {
                filters.Add(filterBuilder.Eq(user => user.dateOfBirth, filter.dateOfBirth.Value));
            }

            //Applied filter for enabled users
            if (filter.onlyEnabled)
            {
                filters.Add(filterBuilder.Eq(user => user.isEnabled, true));
            }

            // Applied filters if necessary
            var combinedFilter = filters.Any() ? filterBuilder.And(filters) : FilterDefinition<User>.Empty;

            // Create order object
            var sortBuilder = Builders<User>.Sort;
            string orderField = "firstName"; //Default sorting field
            if (!string.IsNullOrEmpty(filter.orderField))
            {
                orderField = filter.orderField;
            }
            var sort = Builders<User>.Sort.Ascending(orderField); // Default sorting order

            if (filter.orderDirection == "desc")
            {
                sort = sortBuilder.Descending(orderField);
            }

            // Execute query
            var userListDB = await _usersCollection.Find(combinedFilter).Sort(sort).ToListAsync();

            // Create and map result list
            List<UserDtoOutput> result = _mapper.Map<List<UserDtoOutput>>(userListDB);
            return await Task.FromResult(result);
        }

        public async Task<UserDtoOutput> CreateUser(UserDto newUser)
        {
            User user = ConvertDtoToUser(newUser);  
            await _usersCollection.InsertOneAsync(user);
            return await GetUserById(user.id);
        }

        public async Task<UserDtoOutput> UpdateUser(UserDto userUpdate, string id)
        {
            var filter = Builders<User>.Filter.Eq(u => u.id, id); // Filtra per Id
            User user = ConvertDtoToUser(userUpdate, id);
            await _usersCollection.ReplaceOneAsync(filter, user);
            return await GetUserById(user.id);
        }

        public async Task<UserDtoOutput> GetUserById(string id)
        {
            var filter = Builders<User>.Filter.Eq(u => u.id, id);
            var user = await _usersCollection.Find(filter).FirstOrDefaultAsync();
            return ConvertUserToDto(user);
        }

        public async Task<bool> DeleteUser(string id)
        {
            var filter = Builders<User>.Filter.Eq(u => u.id, id); 
            await _usersCollection.DeleteOneAsync(filter);
            return true;
        }



        private User ConvertDtoToUser(UserDto newUser, string? id = null)
        {
            User user = _mapper.Map<User>(newUser);
            if (!string.IsNullOrEmpty(id))
            {
                user.id = id;
            }
            return user;
        }

        private UserDtoOutput ConvertUserToDto(User user)
        {
            UserDtoOutput userDto = _mapper.Map<UserDtoOutput>(user);
            return userDto;
        }

    }
}

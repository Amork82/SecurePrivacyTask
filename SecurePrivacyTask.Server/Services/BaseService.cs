using AutoMapper;
using SecurePrivacyTask.Server.DBContext;
using SecurePrivacyTask.Server.Dto.Output;

namespace SecurePrivacyTask.Server.Services
{
    public class BaseService
    {
        protected readonly IMapper _mapper;
        protected readonly MongoDBContext _mongoDBContext;

        public BaseService()
        {
            
        }
        public BaseService(IServiceProvider serviceProvider)
        {
            _mapper = serviceProvider.GetRequiredService<IMapper>();
            _mongoDBContext = serviceProvider.GetRequiredService<MongoDBContext>();
        }

        public async Task<BinaryResult> VerifyBinaryString(string binaryValue)
        {
            int counterZero = 0;
            int counterOne = 0;
            BinaryResult binaryResult = new BinaryResult(binaryValue);

            if (string.IsNullOrEmpty(binaryValue))
            {                
                binaryResult.SetError("No value processed");
            }
            else
            {
                int bitPosition = 0;
                // Check for each character
                foreach (char bit in binaryValue)
                {
                    bitPosition++;
                    if (bit == '0')
                    {
                        counterZero++;
                    }
                    else if (bit == '1')
                    {
                        counterOne++;
                    }
                    else
                    {
                        //Check bit value
                        binaryResult.SetError($"The input string contains invalid character on position: {bitPosition}");
                        break;
                    }

                    // Check prefix
                    if (counterOne < counterZero)
                    {
                        binaryResult.SetError($"Prefix counter one less then zero error on position: {bitPosition}");
                        break;
                    }
                }

                if (binaryResult.IsSuccess && counterOne != counterZero)
                {
                    // Check equal number of 0 and 1
                    binaryResult.SetError($"Different count of one ({counterOne}) and zero ({counterZero})");
                }
            }           

            return await Task.FromResult(binaryResult);
        }
    }
}

export interface User {
  id?: string;
  firstName?: string;
  lastName?: string;
  dateOfBirth?: Date;
  country: string;
  city: string;
  isEnabled: boolean;
  requestToBeForgotten: boolean
  consentForDataProcessing: boolean
  consentForReceivingPromotionalMessages: boolean
}

export interface UserFilter {
  firstName?: string;
  lastName?: string;
  dateOfBirth?: Date;
  onlyEnabled: boolean;
  orderField?: string;
  orderDirection?: string;
}

import { Provider } from "./provider";

export interface ProviderBillViewModel {
    billId?: number;
    units: number;
    other: number;
    paid: boolean;
    totalPrice?: number;
    providerUnitPrice?: number;
    providerName?: string;
    providerId?: number;
    dueDate?: string;
}
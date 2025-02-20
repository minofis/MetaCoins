export interface ITransaction
{
    id: number
    coinId: number
    type: string
    status: string
    createdAt: string
    senderWalletId: number
    recipientWalletId: number
}
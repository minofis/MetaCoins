export interface IPaginatedResult<T>
{
    items: T[]
    totalItems: number
    page: number
    pageSize: number
    hasNextPage: boolean
    hasPreviousPage: boolean
}
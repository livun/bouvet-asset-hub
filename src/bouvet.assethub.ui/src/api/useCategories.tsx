import { CategoryResponseDto, CreateCategoryCommand, UpdateCategoryDto } from "../__generated__/api-types"
import { ApiResponse, useApi, useDeleteApi, useGetApi } from "./useApi"

export function useGetCategories(): ApiResponse<CategoryResponseDto[]> {
    return useGetApi<CategoryResponseDto[]>("/categories")
};
export function usePostCategories(dto: CreateCategoryCommand): ApiResponse<CategoryResponseDto> {
    return useApi<CreateCategoryCommand, CategoryResponseDto>("/categories", dto, "POST")
};

export function useGetCategoryById(id: number): ApiResponse<CategoryResponseDto> {
    return useGetApi<CategoryResponseDto>(`/categories/${id}`)
};
export function usePutCategory(id: number, dto: UpdateCategoryDto): ApiResponse<CategoryResponseDto> {
    return useApi<UpdateCategoryDto, CategoryResponseDto>(`/categories/${id}`, dto, "PUT")
};

export function useDeleteCategory(id: number): ApiResponse<CategoryResponseDto> {
    return useDeleteApi<CategoryResponseDto>(`/categories/${id}`)
};
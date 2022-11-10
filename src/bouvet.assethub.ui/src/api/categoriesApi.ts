import { CategoryResponseDto, CreateCategoryCommand, UpdateCategoryDto } from "../__generated__/api-types";
import { deleteItem, get, postItem, putItem, regularHeaders } from "./genericAxios";


export const getCategoriesFn = async () => {
  return await get<CategoryResponseDto[]>(`/categories`)
};
export const getCategoryByIdFn = async (id: number) => {
  return await get<CategoryResponseDto>(`/categories/${id}`)
};
export const postCategoriesFn = async (dto: CreateCategoryCommand) => {
  return await postItem<CreateCategoryCommand, CategoryResponseDto>(`/categories`, dto, regularHeaders)
};
export const putCategoryFn = async (id: number, dto: UpdateCategoryDto) => {
  return await putItem<UpdateCategoryDto, CategoryResponseDto>(`/categories/${id}`, dto, regularHeaders)
};

export const deleteCategoryFn = async (id: number) => {
  return await deleteItem<CategoryResponseDto>(`/categories/${id}`, regularHeaders)
};

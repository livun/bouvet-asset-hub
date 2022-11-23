import { AssetResponseDto, CreateAssetDto, UpdateAssetDto, UpdateAssetsByIdDto } from "../_generated/api-types";
import { deleteItem, get, postItem, putItem, regularHeaders } from "./genericAxios";


export const getAssetsFn = async () => {
  return await get<AssetResponseDto[]>(`/assets`)
};
export const getAssetByIdFn = async (id: number) => {
  return await get<AssetResponseDto>(`/assets/${id}`)
};
export const getAssetByGuidFn = async (guid: string) => {
  return await get<AssetResponseDto>(`/assets/${guid}`)
};
export const postAssetsFn = async (dto: CreateAssetDto) => {
  return await postItem<CreateAssetDto, AssetResponseDto>(`/assets`, dto, regularHeaders)
};

export const putAssetsFn = async (dto: UpdateAssetsByIdDto) => {
  return await putItem<UpdateAssetsByIdDto, AssetResponseDto[]>(`/assets`, dto, regularHeaders)
};

export const putAssetByIdFn = async (id: number, dto: UpdateAssetDto) => {
  return await putItem<UpdateAssetDto, AssetResponseDto>(`/assets/${id}`, dto, regularHeaders)
};

export const deleteAssetFn = async (id: number) => {
  return await deleteItem<AssetResponseDto>(`/assets/${id}`, regularHeaders)
};

export const getAssetsByCategoryFn = async (id: number) => {
  return await get<AssetResponseDto[]>(`/categories/${id}/assets`)
};




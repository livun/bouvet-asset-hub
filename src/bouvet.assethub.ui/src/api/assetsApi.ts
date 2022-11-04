import { AssetResponseDto, CreateAssetCommand, UpdateAssetDto, UpdateAssetsByIdCommand } from "../__generated__/api-types";
import { deleteItem, formHeaders, get, postItem, putItem, regularHeaders } from "./genericAxios";


export const getAssetsFn = async () => {
  return await get<AssetResponseDto[]>(`/assets`)
};
export const getAssetByIdFn = async (id: number) => {
  return await get<AssetResponseDto>(`/assets/${id}`)
};
export const postAssetsFn = async (dto: CreateAssetCommand) => {
  return await postItem<CreateAssetCommand, AssetResponseDto>(`/assets`, dto, regularHeaders)
};

export const putAssetsFn = async (dto: UpdateAssetsByIdCommand) => {
  return await putItem<UpdateAssetsByIdCommand, AssetResponseDto[]>(`/assets`, dto, regularHeaders)
};

export const putAssetByIdFn = async (id: number, dto: UpdateAssetDto) => {
  return await putItem<UpdateAssetDto, AssetResponseDto>(`/assets/${id}`, dto, regularHeaders)
};

export const deleteAssetByIdFn = async (id: number) => {
  return await deleteItem<AssetResponseDto>(`/assets/${id}`, regularHeaders)
};

export const getAssetsByCategory = async (id: number) => {
  return await get<AssetResponseDto[]>(`/categories/${id}/assets`)
};




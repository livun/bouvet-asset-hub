import { AssetResponseDto, CreateAssetCommand, UpdateAssetDto, UpdateAssetsByIdCommand } from "../__generated__/api-types";
import { ApiResponse, useApi, useDeleteApi, useGetApi } from "./useApi";

export function useGetAssets(): ApiResponse<AssetResponseDto[]> {
    return useGetApi<AssetResponseDto[]>("/assets")
};

export function usePostAssets(dto: CreateAssetCommand): ApiResponse<AssetResponseDto> {
    return useApi<CreateAssetCommand, AssetResponseDto>("/assets", dto, "POST")
};

export function usePutAssets(dto: UpdateAssetsByIdCommand): ApiResponse<AssetResponseDto[]> {
    return useApi<UpdateAssetsByIdCommand, AssetResponseDto[]>("/assets", dto, "PUT")
};

export function useGetAssetById(id: number): ApiResponse<AssetResponseDto> {
    return useGetApi<AssetResponseDto>(`/assets/${id}`)
};
export function usePutAssetById(id: number, dto: UpdateAssetDto): ApiResponse<AssetResponseDto> {
    return useApi<UpdateAssetDto, AssetResponseDto>(`/assets/${id}`, dto, "PUT")
};

export function useDeleteAsset(id: number): ApiResponse<AssetResponseDto> {
    return useDeleteApi<AssetResponseDto>(`/assets/${id}`)
};
export function useGetAssetByCategory(id: number): ApiResponse<AssetResponseDto[]> {
    return useGetApi<AssetResponseDto[]>(`/categories/${id}/assets`)
};



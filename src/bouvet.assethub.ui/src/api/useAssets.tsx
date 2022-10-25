import { useEffect, useState } from "react"
import { AssetResponseDto, Client, CreateAssetCommand, UpdateAssetDto, UpdateAssetsByIdCommand } from "../__generated__/api-client"
import { client } from "./Client";

const useAssets = () => {
    const client : Client = new Client("https://localhost:3001")

    const [assets, setAssets] = useState<AssetResponseDto[]>();
    const [asset, setAsset] = useState<AssetResponseDto>();
    const [assetByCategory, setAssetByCategory] = useState<AssetResponseDto[]>(); 
    
    
    const getAssets = async () => {
        const data: AssetResponseDto[] = await client.assetsAllGET();
        setAssets(data);

    };
    const getAssetsTest = async () => {
        const response = await client.assetsAllGET();
        return response;
        

    };
    const postAsset = async (dto : CreateAssetCommand) => {
        const asset = await client.assetsPOST(dto);
        return asset
    }
    
    const putAssets = async (dto : UpdateAssetsByIdCommand) => {
        return await client.assetsAllPUT(dto);
    }
    const getAssetById = async (id: number) => {
        const data: AssetResponseDto = await client.assetsGET(id);
        setAsset(data);
    }
    const putAssetById = async (id : number, dto : UpdateAssetDto) => {
        return await client.assetsPUT(id, dto );
        
    }
    const deleteAsset = async (id : number) => {
        return await client.assetsDELETE(id);
    }
    const getAssetByCategory = async (id : number) => {
        const data: AssetResponseDto[] = await client.assetsAllGET2(id);
        setAssetByCategory(data);
    }
    // useEffect(() => {
    //     getAssets()
    // }, [])

    return {getAssetsTest, assets, asset, assetByCategory, getAssets, postAsset, putAssets, getAssetById, putAssetById, deleteAsset, getAssetByCategory};

};

export default useAssets;


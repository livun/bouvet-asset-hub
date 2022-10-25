import { useState } from "react"
import { CategoryResponseDto,CreateCategoryCommand, UpdateCategoryDto } from "../__generated__/api-client"
import { client } from "./Client";

const useCategories = () => {

    const [categories, setCategories] = useState<CategoryResponseDto[]>();
    const [category, setCategory] = useState<CategoryResponseDto>();
    
    
    const getCategories = async () => {
        const data: CategoryResponseDto[] = await client.categoriesAll();
        setCategories(data);

    };
    const postCategory = async (dto : CreateCategoryCommand) => {
        return await client.categoriesPOST(dto);
    }
    
    const putCategory = async (id : number, dto : UpdateCategoryDto) => {
        return await client.categoriesPUT(id, dto);
    }
    const getCategoryById = async (id: number) => {
        const data: CategoryResponseDto = await client.categoriesGET(id);
        setCategory(data);
    }
   
    const deleteCategories = async (id : number) => {
        return await client.categoriesDELETE(id);
    }

    return {categories, category, getCategories, postCategory, getCategoryById, deleteCategories, putCategory};

};

export default useCategories;


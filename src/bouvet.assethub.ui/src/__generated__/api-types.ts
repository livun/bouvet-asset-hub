/* eslint-disable */
/* tslint:disable */
/*
 * ---------------------------------------------------------------
 * ## THIS FILE WAS GENERATED VIA SWAGGER-TYPESCRIPT-API        ##
 * ##                                                           ##
 * ## AUTHOR: acacode                                           ##
 * ## SOURCE: https://github.com/acacode/swagger-typescript-api ##
 * ---------------------------------------------------------------
 */

export interface AssetResponseDto {
  /** @format int32 */
  id?: number;
  /** @format int32 */
  serialNumberValue?: number;
  categoryName?: string | null;
  status?: Status;
}

export interface CategoryResponseDto {
  /** @format int32 */
  id?: number;
  name?: string | null;
}

export interface CreateAssetCommand {
  /** @format int32 */
  serialNumberValue?: number;
  /** @format int32 */
  categoryId?: number;
}

export interface CreateCategoryCommand {
  name?: string | null;
}

export interface CreateLoanCommand {
  /** @format date-time */
  intervalStart?: string;
  /** @format date-time */
  intervalStop?: string;
  intervalIsLongterm?: boolean;
  /** @format int32 */
  assignedToValue?: number;
  /** @format int32 */
  assetId?: number;
  bsdReference?: string | null;
}

export interface LoanHistoryResponseDto {
  /** @format int32 */
  id?: number;
  /** @format date-time */
  intervalStart?: string;
  /** @format date-time */
  intervalStop?: string;
  /** @format date-time */
  returnDate?: string;
  /** @format int32 */
  borrowerEmployeeNumberValue?: number;
  /** @format int32 */
  assetId?: number;
}

export interface LoanResponseDto {
  /** @format int32 */
  id?: number;
  /** @format date-time */
  intervalStart?: string;
  /** @format date-time */
  intervalStop?: string;
  intervalIsLongterm?: boolean;
  /** @format int32 */
  assignedToValue?: number;
  /** @format int32 */
  assetId?: number;
  assetStatus?: Status;
  assetCategoryName?: string | null;
  bsdReference?: string | null;
}

/**
 * @format int32
 */
export type Status = 0 | 1 | 2 | 3;

export interface UpdateAssetDto {
  status?: Status;
  /** @format int32 */
  categoryId?: number;
}

export interface UpdateAssetsByIdCommand {
  ids?: number[] | null;
  status?: Status;
}

export interface UpdateCategoryDto {
  name?: string | null;
}

export interface UpdateLoanDto {
  /** @format date-time */
  intervalStop?: string;
}

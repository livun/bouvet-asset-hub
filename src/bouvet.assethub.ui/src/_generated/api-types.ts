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
  serialNumberValue?: string;
  /** @format int32 */
  categoryId?: number;
  categoryName?: string;
  status?: Status;
}

export interface CategoryResponseDto {
  /** @format int32 */
  id?: number;
  name?: string;
}

export interface CreateAssetDto {
  serialNumber?: string;
  /** @format int32 */
  categoryId?: number;
  /** @format uuid */
  qrIdentifier?: string;
}

export interface CreateCategoryDto {
  name?: string;
}

export interface CreateLoanDto {
  /** @format date-time */
  intervalStart?: string;
  /** @format date-time */
  intervalStop?: string ;
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
  intervalStop?: string | null;
  intervalIsLongterm?: boolean;
  /** @format int32 */
  assignedToValue?: number;
  /** @format int32 */
  assetId?: number;
  assetStatus?: Status;
  assetCategoryName?: string;
  bsdReference?: string;
}

export enum Status {
  Registered = "Registered",
  Available = "Available",
  Unavailable = "Unavailable",
  Discontinued = "Discontinued",
}

export interface UpdateAssetDto {
  status?: Status;
  /** @format int32 */
  categoryId?: number;
}

export interface UpdateAssetsByIdDto {
  ids?: number[];
  status?: Status;
}

export interface UpdateCategoryDto {
  name?: string;
}

export interface UpdateLoanDto {
  /** @format date-time */
  intervalStop?: string;
}

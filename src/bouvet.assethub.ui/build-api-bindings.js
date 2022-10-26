const { generateApi } = require('swagger-typescript-api');
const path = require("path");
const fs = require("fs");
/* NOTE: all fields are optional expect one of `output`, `url`, `spec` */
generateApi({
  name: "clien.ts",
  output: resolve(process.cwd(), "./src/__generated__"),
  //url: 'https://localhost:3001/swagger/v1/swagger.json',
  input: resolve(process.cwd(), './swagger.json'),
  httpClientType: "axios", // or "fetch"
  defaultResponseAsSuccess: false,
  generateRouteTypes: false,
  generateResponses: true,
  toJS: false,
  extractRequestParams: false,
  extractRequestBody: false,
  prettier: {
    printWidth: 120,
    tabWidth: 2,
    trailingComma: "all",
    parser: "typescript",
  },
  defaultResponseType: "void",
  singleHttpClient: true,
  cleanOutput: false,
  enumNamesAsValues: false,
  moduleNameFirstTag: false,
  generateUnionEnums: false,
  extraTemplates: [],
  hooks: {
    onCreateComponent: (component) => {},
    onCreateRequestParams: (rawType) => {},
    onCreateRoute: (routeData) => {},
    onCreateRouteName: (routeNameInfo, rawRouteInfo) => {},
    onFormatRouteName: (routeInfo, templateRouteName) => {},
    onFormatTypeName: (typeName, rawTypeName) => {},
    onInit: (configuration) => {},
    onParseSchema: (originalSchema, parsedSchema) => {},
    onPrepareConfig: (currentConfiguration) => {},
  }
})

  .catch(e => console.error(e))
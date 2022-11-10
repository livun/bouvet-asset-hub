# Run this command to generate client types/interfaces, (remove NULL on IntervalStop)
npx swagger-typescript-api -p ./src/_generated/swagger.json -o ./src/_generated -n api-types.ts --no-client
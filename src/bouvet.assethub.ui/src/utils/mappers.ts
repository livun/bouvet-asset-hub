export const lookupKeysMapper : any = {
    'serialNumberValue': 'Serial Number',
    'categoryName': 'Category',
    'intervalStart': 'Start Date',
    'intervalStop': 'Stop Date',
    'intervalIsLongterm': 'Longterm',
    'assignedToValue': 'Assigned To',
    'bsdReference': 'BSD',
    'borrowerEmployeeNumberValue' : 'Borrower',
    'assetCategoryName': 'Asset Category',
};
export const routeMapper : any = {
    '/assets': 'Assets',
    '/loans': 'Loans',
    '/loanhistory': 'Loan History'

}

export const statusMapper : any = {
    0: "Registered", 
    1: "Available",
    2:  "Unavailable",
    3:  "Discontinued"
}

export const statusChecker = (value?: string | Number) => {
    const number =  Number(value)
switch (number) {
    case 0:
        return 0
    case 1:
        return 1
    case 2: 
        return 2
    case 3:
        return 3
    default:
        return 0
}
}
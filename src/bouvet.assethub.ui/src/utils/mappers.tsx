import CalendarMonthSharpIcon from '@mui/icons-material/CalendarMonthSharp';
import GridViewSharpIcon from '@mui/icons-material/GridViewSharp';
import SquareSharpIcon from '@mui/icons-material/SquareSharp';
import DashboardCustomizeSharpIcon from '@mui/icons-material/DashboardCustomizeSharp';
import EventAvailableSharpIcon from '@mui/icons-material/EventAvailableSharp';
import CategoryIcon from '@mui/icons-material/Category';


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
    '/loanhistory': 'Loan History',
    '/categories': 'Categories'

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

export const headerIcons : any = {
    "Categories": <CategoryIcon fontSize='large'  />,
    "Loans": <CalendarMonthSharpIcon fontSize='large' />,
    "Assets": <GridViewSharpIcon fontSize='large' />,
    // "Assets by Category": <SquareSharpIcon fontSize='large' />,
    "Loan History": <EventAvailableSharpIcon fontSize='large' />
};
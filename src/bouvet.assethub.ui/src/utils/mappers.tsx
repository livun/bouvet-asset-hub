import CalendarMonthSharpIcon from '@mui/icons-material/CalendarMonthSharp';
import GridViewSharpIcon from '@mui/icons-material/GridViewSharp';
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
export const headerIcons : any = {
    "Categories": <CategoryIcon fontSize='large'  />,
    "Loans": <CalendarMonthSharpIcon fontSize='large' />,
    "Assets": <GridViewSharpIcon fontSize='large' />,
    "Loan History": <EventAvailableSharpIcon fontSize='large' />
};
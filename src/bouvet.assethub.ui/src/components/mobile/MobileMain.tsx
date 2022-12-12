import { styled } from '@mui/material/styles';

const drawerWidth = 240;
const Body = styled('main', { shouldForwardProp: (prop) => prop !== 'open' })<{
	open?: boolean;
}>(({ theme, open }) => ({
	transition: theme.transitions.create('margin', {
		easing: theme.transitions.easing.sharp,
		duration: theme.transitions.duration.leavingScreen,
	}),
	marginLeft: `-${drawerWidth}px`,
	...(open && {
		transition: theme.transitions.create('margin', {
			easing: theme.transitions.easing.easeOut,
			duration: theme.transitions.duration.enteringScreen,
		}),
		marginLeft: 0,
	}),
}));

export const DrawerHeader = styled('div')(({ theme }) => ({
	display: 'flex',
	alignItems: 'center',
	...theme.mixins.toolbar,
	justifyContent: 'flex-end',
}));

export default function MobileMain(prop: { open: boolean }) {

	return <>
		<Body open={prop.open}>
			<DrawerHeader />
		</Body>
	</>
}
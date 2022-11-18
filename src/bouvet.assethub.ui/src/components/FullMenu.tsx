import { styled, useTheme } from '@mui/material/styles';
import Drawer from '@mui/material/Drawer';
import CssBaseline from '@mui/material/CssBaseline';
import MuiAppBar, { AppBarProps as MuiAppBarProps } from '@mui/material/AppBar';
import Toolbar from '@mui/material/Toolbar';
import List from '@mui/material/List';
import Typography from '@mui/material/Typography';
import Divider from '@mui/material/Divider';
import IconButton from '@mui/material/IconButton';
import MenuIcon from '@mui/icons-material/Menu';
import ChevronRightIcon from '@mui/icons-material/ChevronRight';
import ListItem from '@mui/material/ListItem';
import ListItemButton from '@mui/material/ListItemButton';
import ListItemText from '@mui/material/ListItemText';
import { DrawerHeader } from './Main';
import { Link } from 'react-router-dom';
import { capitalizeAndSplit } from '../utils/regex';
import CloseIcon from '@mui/icons-material/Close';

const drawerWidth = 240;

const pages = ['Assets', 'Loans', 'LoanHistory']
const other = ['Categories']
const actions = ["Mobile", "QR"]

interface AppBarProps extends MuiAppBarProps {
	open?: boolean;
}

const AppBar = styled(MuiAppBar, {
	shouldForwardProp: (prop) => prop !== 'open',
})<AppBarProps>(({ theme, open }) => ({
	transition: theme.transitions.create(['margin', 'width'], {
		easing: theme.transitions.easing.sharp,
		duration: theme.transitions.duration.leavingScreen,
	}),
	...(open && {
		width: `calc(100% - ${drawerWidth}px)`,
		marginLeft: `${drawerWidth}px`,
		transition: theme.transitions.create(['margin', 'width'], {
			easing: theme.transitions.easing.easeOut,
			duration: theme.transitions.duration.enteringScreen,
		}),
	}),
}));


export default function FullMenu(prop: { open: boolean, handleOpen: () => void }) {
	const theme = useTheme();
	const { open, handleOpen } = prop

	return (
		<>
			<CssBaseline />
			<AppBar position="fixed" open={open}>
				<Toolbar>
					<IconButton
						color="inherit"
						aria-label="open drawer"
						onClick={handleOpen}
						edge="start"
						sx={{ mr: 2, ...(open && { display: 'none' }) }}
					>
						<MenuIcon />
					</IconButton>
					<Typography variant="h6" noWrap component="div" flexGrow={1}>
						<Link to={'/test'} style={{ color: 'inherit', textDecoration: 'none' }}>
							Bouvet Asset Hub
						</Link>
					</Typography>
					<Typography variant="h6" noWrap component="div" px={1}>
						<Link to={'/test'} style={{ color: 'inherit', textDecoration: 'none' }}>
							LINK
						</Link>
					</Typography>
					<Typography variant="h6" noWrap component="div" px={1}>
						<Link to={'/test'} style={{ color: 'inherit', textDecoration: 'none' }}>
							LINK
						</Link>
					</Typography>
					<Typography variant="h6" noWrap component="div" px={1} >
						<Link to={'/test'} style={{ color: 'inherit', textDecoration: 'none' }}>
							LINK
						</Link>
					</Typography>
				</Toolbar>
			</AppBar>
			<Drawer
				sx={{
					width: drawerWidth,
					flexShrink: 0,
					'& .MuiDrawer-paper': {
						width: drawerWidth,
						boxSizing: 'border-box',
					},
				}}
				variant="persistent"
				anchor="left"
				open={open}
			>
				<DrawerHeader>
					<IconButton onClick={handleOpen}>
						{theme.direction === 'ltr' ? <CloseIcon /> : <ChevronRightIcon />}
					</IconButton>
				</DrawerHeader>
				<Divider />
				<List>
					{pages.map((page, index) => (
						<Link key={index} to={`/${page.toLocaleLowerCase()}`} style={{ color: 'inherit', textDecoration: 'none' }}>
							<ListItem key={index} disablePadding>
								<ListItemButton>
									<ListItemText primary={capitalizeAndSplit(page)} />
								</ListItemButton>
							</ListItem>
						</Link>
					))}
				</List>
				<Divider />

				<List>
					{other.map((page, index) => (
						<Link key={index} to={`/${page.toLocaleLowerCase()}`} style={{ color: 'inherit', textDecoration: 'none' }}>
						<ListItem key={index} disablePadding>
							<ListItemButton>
								<ListItemText primary={capitalizeAndSplit(page)} />
							</ListItemButton>
						</ListItem>
					</Link>
					))}
				</List>
				<Divider />

				<List>
					{actions.map((action, index) => (
						<Link key={index} to={`/${action.toLocaleLowerCase()}`} style={{ color: 'inherit', textDecoration: 'none' }}>

						<ListItem key={index} disablePadding>
							<ListItemButton>
								<ListItemText primary={(action)} />
							</ListItemButton>
						</ListItem>
						</Link>
					))}
				</List>
			</Drawer>

		</>
	);
}

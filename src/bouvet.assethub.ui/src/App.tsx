import React from 'react';
import logo from './logo.svg';
import './App.css';

import { Box } from '@mui/material';
import FullMenu from './components/FullMenu';
import Main from './components/Main';
import { Route, Routes } from 'react-router-dom';
import Assets from './routes/Assets';
import Loans from './routes/Loans';
import LoanHistory from './routes/LoanHistory';


function App() {
  const [open, setOpen] = React.useState(false);
  const handleOpen = () => {
    setOpen(!open)
  }
  return (
    <>
    <Box sx={{ display: 'flex' }}>
      <FullMenu open={open} handleOpen={handleOpen} />
      

      <Routes>
          <Route path='assets' element={<Main open={open} child={<Assets/>}/>} />	
          <Route path='loans' element={<Main open={open} child={<Loans/>}/>} />	
          <Route path='loanhistory' element={<Main open={open} child={<LoanHistory/>}/>} />	
        </Routes>
    </Box>
   

    </>
 
  );
}

export default App;

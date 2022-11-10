import React, { useState } from 'react';
import './App.css';
import { Box } from '@mui/material';
import FullMenu from './components/FullMenu';
import Main from './components/Main';
import { Route, Routes } from 'react-router-dom';
import Assets from './views/Assets';
import Loans from './views/Loans';
import LoanHistory from './views/LoanHistory';
import Asset from './views/Asset';
import Loan from './views/Loan';
import Categories from './views/Categories';


function App() {
  const [open, setOpen] = useState(false);
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
          <Route path='assets/:id' element={<Main open={open} child={<Asset />} />} />	
          <Route path='loans/:id' element={<Main open={open} child={<Loan />} />} />	
          <Route path='categories' element={<Main open={open} child={<Categories />} />} />
        
        </Routes>
    </Box>
   

    </>
 
  );
}

export default App;

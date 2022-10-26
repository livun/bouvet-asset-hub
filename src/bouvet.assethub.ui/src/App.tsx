import React from 'react';
import logo from './logo.svg';
import './App.css';

import { Box } from '@mui/material';
import FullMenu from './components/FullMenu';
import Main from './components/Main';
import { Route, Routes } from 'react-router-dom';
import { Test } from './components/test';
import Assets from './routes/Assets';


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
          <Route path='Assets' element={<Main open={open} child={<Assets/>}/>} />	
          <Route path='test' element={<Main open={open} child={<Test/>}/>} />	
          <Route path='test' element={<Main open={open} child={<Test/>}/>} />	
        </Routes>
    </Box>
   

    </>
 
  );
}

export default App;

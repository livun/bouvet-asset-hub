import React from 'react';
import logo from './logo.svg';
import './App.css';
import WebcamComponent from './components/WebcamComponent';
import QRScanner from './components/QRScanner';
// import { QRScanner } from './QRScanner';

function App() {
  
  return (
    <div className="App">
      <header className="App-header">
        <QRScanner />
      
      </header>
    </div>
  );
}

export default App;

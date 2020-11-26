import React from 'react';
import {Link} from 'react-router-dom';
import './styles.css';


const ButtonComponent = ({value ,link, className, type}) => {

  return (
    <>
    { link ? (<Link to={link}>
      <button type={type ? type : "button"} className={`btn ${className}`}>{value}</button>
    </Link>) : (<button type={type ? type : "button"} className={`btn ${className}`}>{value}</button>)}
    </>  
  )
}

export default ButtonComponent;
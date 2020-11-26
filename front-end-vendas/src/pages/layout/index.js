import React, { useState } from 'react';
import { Link, useHistory } from 'react-router-dom';
import { useCartContext } from '../../context/productContext';
import { SearchOutlined, HeartOutlined, ShoppingCartOutlined, MenuOutlined } from '@ant-design/icons';
import './styles.css';

export const LayoutPage = ({ children }) => {
  const [menuVisibility, setMenuvisibility] = useState(false);
  const { products } = useCartContext();
  const [search, setSearch] = useState("");
  const history =useHistory();

  function handleMenuVisibility() {
    setMenuvisibility(!menuVisibility);
  }

  function handleChangeText(e){
    setSearch(e.target.value);
  }

  function handleKeyPress(e){
    if(e.key === "Enter"){
      setSearch("");
      history.push(`/search?q=${e.target.value}`)
    }
  }

  function menuMobile() {
    if (menuVisibility)
      return (
        <div className="optionsMobile">
          <div className="searchMobile">
            <input type="text" value={search} onKeyPress={e => handleKeyPress(e)} onChange={e=> handleChangeText(e)} className="inputSearch" name="q" id="q" placeholder="O que você está procurando?"/>
            <button type="button" className="btnSearch" >
              <SearchOutlined />
            </button>
          </div>
          <div className="categoriesMobile">
            <ul>
              <li className="menuOptions">
                <Link to='/favorites'>
                  <div className="favoritos">
                    <HeartOutlined /> Favoritos
                    </div>
                </Link>
                <Link to='/cart'>
                  <div className="carrinho">
                    <div className="Badge">{products?.length}</div>
                    <ShoppingCartOutlined />
                  </div>
                </Link>
                <div className="entrar">
                  Entrar
                  </div>
              </li>
              <li> <MenuOutlined /> Todos os produtos</li>
            </ul>
          </div>
        </div>
      )
    return (null);
  }

  return (
    <>
      <nav className="header">
        <div className="menuMobile" onClick={handleMenuVisibility}>
          <MenuOutlined />
        </div>
        <div className="logo">
          <Link to='/products'>
            <h1>Fut-arena</h1>
          </Link>
        </div>
        <div className="search">
            <input type="text" value={search} onKeyPress={e => handleKeyPress(e)} onChange={e=> handleChangeText(e)} className="inputSearch" name="q" id="q" placeholder="O que você está procurando?" />
            <Link to={`/search?q=${search}`}>
              <button type="button" className="btnSearch" >
                <SearchOutlined />
              </button>
            </Link>
        </div>
        <div className="options">
          <Link to='/favorites'>
            <div className="favoritos">
              <HeartOutlined /> Favoritos
          </div>
          </Link>
          <Link to='/cart'>
            <div className="carrinho" >
              <div className="Badge">{products?.length}</div>
              <ShoppingCartOutlined />
            </div>
          </Link>
          <div className="entrar">
            Entrar
          </div>
        </div>
      </nav>
      <div className="categories">
        <ul>
          <li>Todos os produtos</li>
        </ul>
      </div>
      {menuMobile()}
      <main className="container">
        {children}
      </main>
      <footer className="footer">
        <span>Todos os direitos Reservados &copy; 2020</span>
      </footer>
    </>
  );
}

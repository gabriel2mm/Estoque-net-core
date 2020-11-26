import React, { useState, useEffect } from 'react';
import { Divider } from 'antd';
import { useCartContext } from '../../context/productContext';
import { HeartOutlined, ShoppingCartOutlined } from '@ant-design/icons';
import './styles.css';

const CardComponent = ({ product }) => {
  const { addProduct } = useCartContext();
  const [favorites, setFavorites] = useState([]);

  useEffect(() => {
    const favs = localStorage.getItem("@Favorites");
    setFavorites(favs ? JSON.parse(favs) : []);
  }, []);

  function handleAddFavorites() {
    const favItem = localStorage.getItem("@Favorites");
    if (favItem) {
      const arrayFavorites = JSON.parse(favItem);
      if (arrayFavorites.filter((i) => i.id === product.id).length <= 0) {
        arrayFavorites.push(product);
        setFavorites(arrayFavorites);
        localStorage.setItem("@Favorites", JSON.stringify(arrayFavorites));
      }
    } else {
      const arrayFavorites = [product];
      localStorage.setItem("@Favorites", JSON.stringify(arrayFavorites));
      setFavorites(arrayFavorites);
    }
  }

  function removeFavorite() {
    const favItem = localStorage.getItem("@Favorites");
    if (favItem) {
      const arrayItems = JSON.parse(favItem);
      const newFavorites = arrayItems.filter((i) => i.id !== product.id);
      localStorage.setItem("@Favorites", JSON.stringify(newFavorites));
      setFavorites(newFavorites);
    }
  }

  function verifyFavorite() {
    if (favorites.filter((i) => i.id === product.id).length <= 0) {
      return (
        <div className="fav" onClick={handleAddFavorites}>
          <HeartOutlined />
        </div>
      )
    }

    return (
      <div className="fav fav-selected" onClick={removeFavorite}>
        <HeartOutlined />
      </div>
    )
  }

  function handleAddCart() {
    addProduct({ ...product, quantidade: 1 });
  }

  return (
    <>
      <div className={`card ${product.productManagement.amount <= 0 ? 'out-of-stock' : ''}`}>
        <div className="image">
          <img src={`https://imagensshowcase.s3-sa-east-1.amazonaws.com/imagens/${product.image}`} alt={product.productManagement.product.name} />
        </div>
        <div className="title">{product.productManagement.product.name}</div>
        <div className="description">{product.description}</div>
        {verifyFavorite()}
        <div className="actions">
          {product.productManagement.amount <= 0 ?
            (<div className="out-of-stock-text">
              <span>Fora de estoque</span>
            </div>):
            (<>
              <div className="preco">
                <div className="brl">{product.productManagement.product.price.toLocaleString('pt-br', { style: 'currency', currency: 'BRL' })}</div>
              </div>
              <Divider type="vertical" />
              <div className="textCarrinho" onClick={handleAddCart}> <ShoppingCartOutlined /> Adicionar ao Carrinho</div>
            </>)}
        </div>
      </div>
    </>
  )
}

export default CardComponent;
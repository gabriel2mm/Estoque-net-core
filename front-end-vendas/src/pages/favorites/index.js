import React, {useState, useEffect} from 'react';
import CardListComponent from '../../components/cardListComponent/index';
import "./styles.css"

const FavoritesPage = () => {
  const [products , setProducts] = useState([]);

  useEffect(() => {
    async function getProducts() {
      const response = localStorage.getItem("@Favorites");
      if (response) {
        const array = JSON.parse(response);
        setProducts(array);
      }
    }
    getProducts();
  }, []);

  return (
    <>
      <section id="produtos">
        <div className="title">
          <h1>Favoritos</h1>
          <CardListComponent products={products}/>
        </div>
      </section>
    </>
  )
}

export default FavoritesPage;
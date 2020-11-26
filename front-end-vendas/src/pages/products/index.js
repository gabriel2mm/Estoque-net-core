import React, { useEffect, useState } from 'react';
import CardListComponent from '../../components/cardListComponent/index';
import CarrouselComponent from '../../components/carrouselComponent';
import {API} from '../../services/API';
import "./styles.css"

const ProductsPage = () => {
  const [products, setProducts] = useState([]);

  useEffect(() => {
    async function getProducts() {
      const response = await API.get('/api/showcases');
      setProducts(response.data);
    }

    getProducts();

  }, []);

  return (
    <section id="produtos">
      <div className="title">
        <h1>Destaques</h1>
        <CarrouselComponent/>
      </div>

      <div className="title">
        <h1>Todos os produtos</h1>
        <CardListComponent products={products}/>
      </div>
    </section>
  )
}

export default ProductsPage;
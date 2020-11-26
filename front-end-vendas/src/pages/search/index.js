import React, {useEffect, useState} from 'react';
import CardListComponent from '../../components/cardListComponent/index';
import {API} from '../../services/API';
import "./styles.css";



const SearchPage = () => {
  const [products, setProducts] = useState([]);
  const queryString = window.location.search;
  useEffect(()=>{

    async function search(){
      
      const urlParams = new URLSearchParams(queryString);
      const search = urlParams.get('q');
  
      const response = await API.get(`/api/Showcases/search/${search}`);
      setProducts(response.data);
    }

    search();
  }, [queryString]);

  return (
    <section id="busca">
      <div className="title">
        <h1>Resultado da sua busca</h1>
        <CardListComponent products={products}/>
      </div>
    </section>
  );
}

export default SearchPage;
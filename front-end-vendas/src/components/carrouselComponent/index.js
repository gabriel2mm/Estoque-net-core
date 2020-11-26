import React, { useEffect, useState } from 'react';
import { API } from '../../services/API';
import { Carousel } from 'antd';
import "./styles.css";

function CarrouselComponent() {

  const [campaign, setCampaign] = useState([])

  useEffect(() => {
    API.get('/api/Campaigns').then(response => setCampaign(response.data)).catch(err => console.error(err));
  }, []);

  return (
    <div className="carrosel">
      <Carousel style={{ height: "300px" }}>
        {campaign.map((s, index) => (
          <div key={index}>
            <div className="img">
              <img src={`https://imagensshowcase.s3-sa-east-1.amazonaws.com/imagens/${s.imageBanner}`} alt="" />
            </div>
            <div className="painel">
              <div className="title">
                <h1>{s.showcase.productManagement.product.name}</h1>
              </div>
              <div className="desc">
                <p>{s.showcase.description}</p>
              </div>
            </div>
          </div>
        ))}
      </Carousel>
    </div>
  );
}

export default CarrouselComponent;
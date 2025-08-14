import React, { useState, useEffect } from 'react';
import { Container, Row, Col, Button, Card } from 'react-bootstrap';
import { getCamperas } from '../../helpers/camperasService'; // Asegúrate de que el servicio esté correctamente importado
import { buscarUsuario } from '../../helpers/usuarios/usuariosService'; // Asegúrate de que el servicio esté correctamente importado
import { getimgURLporID } from "../../helpers/imagenService";
import { obtenerValoracionTotal } from '../../helpers/reviewService';
import StarRatings from 'react-star-ratings';

const VerMasCamperas = () => {
  const [camperas, setCamperas] = useState([]);
  const [loading, setLoading] = useState(true);
  const [mensaje, setMensaje] = useState("");

  useEffect(() => {
    const fetchCamperas = async () => {
      try {
        setLoading(true);
        const respuesta = await getCamperas();
        
        if (respuesta?.datos && Array.isArray(respuesta.datos)) {
          // Crear un mapeo de camperas con nombres de usuario
          const camperasConUsuarios = await Promise.all(
            respuesta.datos.map(async (item) => {
              if (item.usuarioID) {
                try {
                  const usuarioRespuesta = await buscarUsuario(item.usuarioID);
                  console.log(`Respuesta de buscarUsuario para ID ${item.usuarioID}: `, usuarioRespuesta);

                  if (usuarioRespuesta?.data?.exito) {
                    const imageUrl = await getimgURLporID(item.imagenID);
                    const nombreUsuario = usuarioRespuesta?.data?.datos?.nombreUsuario;
                    const valoracionTotal = await obtenerValoracionTotal(item.postID);
                    if (nombreUsuario) {
                      return { ...item, creador: nombreUsuario, imageUrl, valoracionTotal };
                    } else {
                      console.error(`El usuario con ID ${item.usuarioID} no tiene un nombre de usuario válido.`);
                      return { ...item, creador: "Desconocido", imageUrl };
                    }
                  } else {
                    console.error(`No se pudo encontrar el usuario con ID ${item.usuarioID}`);
                    return { ...item, creador: "Desconocido", imageUrl };
                  }
                } catch (error) {
                  console.error(`Error al buscar usuario con ID ${item.usuarioID}:`, error);
                  return { ...item, creador: "Desconocido" };
                }
              } else {
                return { ...item, creador: "Desconocido" };
              }
            })
          );

          setCamperas(camperasConUsuarios);
        } else {
          setMensaje("No se encontraron camperas.");
        }
      } catch (error) {
        console.error("Error al obtener las camperas:", error);
        setMensaje("Hubo un problema al obtener las camperas.");
      } finally {
        setLoading(false);
      }
    };

    fetchCamperas();
  }, []);

  const formatPrice = (price) => {
    return `$ ${price.toFixed(3)}`;
  };

  if (loading) {
    return <Container className="text-center mt-5"><p>Cargando camperas...</p></Container>;
  }

  return (
    <Container fluid className="px-4">
      <h3 className="my-4">Camperas</h3>

      {mensaje ? (
        <p>{mensaje}</p>
      ) : (
        <Row className="g-4">
          {camperas.map((item) => (
            <Col key={item.id} xs={12} sm={6} lg={3}>
              <Card className="product-card h-100">
                <Card.Img 
                  variant="top" 
                  src={item.imageUrl} 
                  alt={item.nombre}
                  style={{ aspectRatio: "1", objectFit: "cover" }}
                />
                <Card.Body className="d-flex flex-column">
                  <h5 className="card-title">{item.nombre}</h5>
                  <div className="mt-3">
                    <StarRatings
                      rating={parseFloat(item.valoracionTotal) || 0}
                      starRatedColor="#FFD700"
                      numberOfStars={5}
                      starDimension="25px"
                      starSpacing="3px"
                    />
                    <span className="ms-2">({item.valoracionTotal})</span>
                  </div>
                  <p className="card-text"><strong>Precio: </strong>{formatPrice(item.precio)}</p>
                  <p className="card-text"><strong>Creador: </strong>{item.creador}</p>
                  <Button variant="primary" href={`/campera/${item.id}`} className="mt-auto">
                    Ver campera
                  </Button>
                </Card.Body>
              </Card>
            </Col>
          ))}
        </Row>
      )}
    </Container>
  );
};

export default VerMasCamperas;

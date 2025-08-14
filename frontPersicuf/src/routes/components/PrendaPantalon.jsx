import React, { useEffect, useState, useRef } from 'react';
import { useParams, useNavigate } from 'react-router-dom';  // Importamos useNavigate para redirección
import 'bootstrap/dist/css/bootstrap.min.css';
import { getPrendaPorID} from '../../helpers/prendasService';
import { getPantalonPorID } from '../../helpers/pantalonesService'
import { buscarColorPorID } from '../../helpers/coloresService';
import { buscarRubroPorID } from '../../helpers/rubroService';
import { buscarMaterialPorID } from '../../helpers/materialService';
import { buscarUsuario } from '../../helpers/usuarios/usuariosService';
import { getTalleAlfabeticoPorID } from '../../helpers/TAService'
import { getUbicacionPorID } from '../../helpers/ubicacionesService';
import { getImagenPorID , getimgURLporID } from '../../helpers/imagenService'
import { getLargoPorID } from '../../helpers/largoService';
import ProductViewer from './persopantalon/ProductViewer';
import { obtenerReview, obtenerValoracionTotal, obtenerNombreUsuarioReview } from '../../helpers/reviewService';
import StarRatings from 'react-star-ratings';

  const Pantalon = () => {
  const { id } = useParams();
  const navigate = useNavigate();
  const [Pantalon, setPantalon] = useState(null);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);
  const [color, setColor] = useState('');
  const [rubro, setRubro] = useState('');
  const [material, setMaterial] = useState('');
  const [usuario, setUsuario] = useState('');
  const [ta, setTa] = useState('');
  const [largo, setLargo] = useState('');
  const [cantidad, setCantidad] = useState(1);
  const viewerRef = useRef(null);
  const [codigoColor, setCodigoColor] = useState('');
  const [path, setPath] = useState('');
  const [posicion, setPosicion] = useState('');
  const [pos2, setPos2] = useState('');
  const [path2, setPath2] = useState('');
  const [imagenDirrecion, setImagenDireccion] = useState('');
  const [reseñas, setReseñas] = useState([]);
  const [valoracionTotal, setValoracionTotal] = useState(0);

  const handleComprarAhora = () => {
    // Redirigir a DetallesPedido pasando la prenda y la cantidad seleccionada
    navigate('/detalles-pedido', {
      state: {
        prenda: Pantalon,
        cantidad: cantidad,
        total: Pantalon.datos.precio * cantidad, 
        imagenDirrecion
      }
    });
  };

  useEffect(() => {
    const fetchPantalon = async () => {
        try {
          setLoading(true);
          const data = await getPrendaPorID(id);
          const dataP = await getPantalonPorID(id);
          setPantalon(data);
      
          const colorData = await buscarColorPorID(data.datos.colorID);  
          const rubroData = await buscarRubroPorID(data.datos.rubroID);  
          const materialData = await buscarMaterialPorID(data.datos.materialID);  
          const usuarioData = await buscarUsuario(data.datos.usuarioID);
          const taData = await getTalleAlfabeticoPorID(dataP.talleAlfabeticoID);
          const estampadoData = await getImagenPorID(dataP.estampadoID);
          const largoData = await getLargoPorID(dataP.largoID);
          const imagenData = await getimgURLporID(data.datos.imagenID);
          let ubicacionData = ('');
          if (estampadoData != null){
            ubicacionData = await getUbicacionPorID(estampadoData.ubicacionID);
          }
      
          if (usuarioData && usuarioData.data && usuarioData.data.datos && usuarioData.data.datos.nombreUsuario) {
            setUsuario(usuarioData.data.datos.nombreUsuario); // Accede correctamente a nombreUsuario
          } else {
            console.error('No se encontró nombreUsuario en los datos de usuario');
            setUsuario('Usuario no encontrado');
          }
      
          setColor(colorData.nombre); 
          setCodigoColor(colorData.codigoHexa);
          setRubro(rubroData.descripcion);  
          setMaterial(materialData.descripcion); 
          setTa(taData.descripcion);
          setLargo(largoData.descripcion);
          setImagenDireccion(imagenData);
          if (estampadoData != null){
            if (ubicacionData.descripcion == "Detrás"){
             setPos2(ubicacionData.descripcion);
             setPath2(estampadoData.path);
             setPath('');
             setPosicion("Bolsillo izquierdo");
            } else {
             setPos2("Detrás");
             setPath2('');
             setPath(estampadoData.path);
             setPosicion(ubicacionData.descripcion);
            }
          } else {
            setPath('');
            setPosicion('Bolsillo izquierdo')
            setPath2('')
            setPos2('Detrás')

          }

          // Obtener reseñas y nombres de usuario
          const reseñasData = await obtenerReview(data.datos.postID);
          setValoracionTotal(await obtenerValoracionTotal(data.datos.postID));

          if (reseñasData && Array.isArray(reseñasData)) {
            // Obtener los nombres de usuario
            const reseñasConNombres = await Promise.all(
              reseñasData.map(async (reseña) => {
                const nombreUsuario = await obtenerNombreUsuarioReview(reseña.owner);
                return { ...reseña, owner: nombreUsuario || 'Usuario no disponible' };
              })
            );
            setReseñas(reseñasConNombres);
          } else {
            setReseñas([]);
          }          
          
      
          setLoading(false);
        } catch (err) {
          console.error("Error al cargar los detalles de la prenda:", err);
          setError('Error al cargar los detalles de la prenda.');
          setLoading(false);
        }
      };
    fetchPantalon();
  }, [id]);

  if (loading) return <div>Cargando...</div>;
  if (error) return <div>{error}</div>;
  if (!Pantalon) return <div>No se encontró el pantalón.</div>;

  const { nombre, precio, descripcion, image} = Pantalon.datos;



  return (
    <div className="container py-4">
      <div className="row">
        <div className="col-md-5">
        <ProductViewer
            ref={viewerRef}
            color={codigoColor}
            uploadedImage={path}
            imagePosition={posicion}
            selectedCategory={rubro}
            selectedLarge={largo}
          />
        </div>
        <div className="col-md-6">
        <ProductViewer
         color={codigoColor}
         uploadedImage={path2}
         imagePosition={pos2} // Aquí aseguramos que la vista sea trasera
         selectedCategory={rubro}
         selectedLarge={largo}
        />
        </div>

        <div className="col-md-7">
          <h1 className="h2">{nombre}</h1>
          <div className="mt-3">
            <StarRatings
              rating={parseFloat(valoracionTotal) || 0}
              starRatedColor="#FFD700"
              numberOfStars={5}
              starDimension="25px"
              starSpacing="3px"
            />
            <span className="ms-2">({valoracionTotal})</span>
          </div>
          <p><strong>Precio:</strong> $ {precio}</p>
          <p>{descripcion}</p>

          {/* Mostrar los nombres en lugar de los IDs */}
          <p><strong>Color:</strong> {color}</p>
          <p><strong>Rubro:</strong> {rubro}</p>
          <p><strong>Material:</strong> {material}</p>
          <p><strong>Usuario:</strong> {usuario}</p>
          <p><strong>Talle:</strong> {ta}</p>
          <p><strong>Largo:</strong> {largo}</p>

           {/* Selector de cantidad */}
         <div className="mt-3">
          <label><strong>Cantidad:</strong></label>
          <input
              type="number"
              className="form-control w-25 d-inline mx-2"
              value={cantidad}
              onChange={(e) => setCantidad(Math.max(1, parseInt(e.target.value) || 1))}
              min="1"
            />
         </div>
          <button className="btn btn-primary mt-4" onClick={handleComprarAhora}>Comprar ahora</button>
          <button className="btn btn-primary mt-4" onClick={() => window.open(`http://localhost:8000/post/${Pantalon.datos.postID}`, '_blank')}>Enviar Reseña</button>

          {/* Mostrar las reseñas */}
          <div className="mt-5">
            <h3>Comentarios:</h3>
            {reseñas.length > 0 ? (
              reseñas.map((reseña, index) => (
                <div key={index} className="mb-4 p-3 border rounded" style={{ backgroundColor: "#f8f9fa" }}>
                  <h5>{reseña.owner}</h5>
                  <p>{reseña.comment}</p>
                  <StarRatings rating={reseña.rating} starRatedColor="#FFD700" numberOfStars={5} starDimension="20px" starSpacing="3px" />
                </div>
              ))
            ) : (
              <p>No hay comentarios disponibles.</p>
            )}
          </div>
        </div>
      </div>
    </div>
  );
};
       

export default Pantalon;
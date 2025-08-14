# Persicuf - Plataforma E-commerce de Personalización de Ropa

![Persicuf Home](https://i.imgur.com/k6tP4Lh.png)

Persicuf es un proyecto full-stack que simula una plataforma de e-commerce completa, permitiendo a los usuarios registrarse, personalizar prendas de vestir, gestionar sus pedidos y realizar compras. El sistema está compuesto por un backend robusto desarrollado en .NET y un frontend moderno y reactivo construido con React.

Este repositorio documenta la arquitectura, las tecnologías y las funcionalidades clave del proyecto, desarrollado como parte de un trabajo académico en equipo.

## Arquitectura y Tecnologías

El proyecto sigue una arquitectura de software moderna, desacoplando el backend y el frontend para lograr una mayor escalabilidad y mantenibilidad.

### **Backend**

El backend es una API RESTful construida con C# y .NET, siguiendo una arquitectura de capas bien definida.

* **Framework:** .NET / ASP.NET Core
* **Lenguaje:** C#
* **Base de Datos:** PostgreSQL
* **ORM:** Entity Framework, utilizando la metodología **Code First** para generar el esquema de la base de datos a partir de los modelos de C#.
* **Arquitectura:**
    * **Capas de Servicios e Interfaces:** Encapsulan toda la lógica de negocio, promoviendo un código modular y testeable.
    * **Inyección de Dependencias:** Utilizada para desacoplar los componentes de la aplicación.
    * **DTOs (Data Transfer Objects):** Para controlar con precisión los datos expuestos por la API y optimizar la comunicación con el frontend.
* **Librerías Clave:**
    * **Mapster:** Para el mapeo eficiente entre modelos de entidad y DTOs.
    * **BCrypt.NET:** Para el hashing seguro de las contraseñas de los usuarios.

### **Frontend**

El frontend es una Single-Page Application (SPA) que ofrece una experiencia de usuario fluida y dinámica.

* **Librería Principal:** React
* **Entorno de Desarrollo:** Vite, para un desarrollo y compilación ultrarrápidos.
* **Gestión de Formularios:** React Hook Form y Yup para la creación y validación de formularios complejos como el de registro.
* **Componentes de UI:** Carruseles dinámicos con `slick-carousel` y alertas de navegación con `react-bootstrap`.
* **Lógica de Componentes:** Uso extensivo de Hooks de React (`useState`, `useEffect`, `useContext`) y creación de Custom Hooks (ej. `usePersonalizacionRemeras`) para encapsular y reutilizar lógica compleja.

## Características Destacadas

El proyecto implementa una serie de funcionalidades avanzadas que demuestran un enfoque integral en el desarrollo de software.

#### **Seguridad con JWT (JSON Web Tokens)**
Se implementó un sistema de autenticación y autorización completo basado en JWT. El backend genera tokens firmados con una clave secreta (HMAC SHA-256) que incluyen los `claims` del usuario (nombre, rol, etc.). La API protege endpoints específicos, permitiendo, por ejemplo, que solo usuarios con el rol de "Admin" puedan ejecutar ciertas acciones. En el frontend, se valida la expiración del token para gestionar la sesión del usuario de forma segura.

#### **Cálculo de Precios Dual**
Para ofrecer una experiencia de usuario instantánea, el precio de las prendas personalizadas se calcula en tiempo real en el **frontend**. Sin embargo, para garantizar la integridad y evitar manipulaciones, el cálculo final y definitivo siempre se realiza y se valida en el **backend** antes de procesar un pedido.

#### **Integración con APIs Externas**
La plataforma se integra con servicios de terceros para extender su funcionalidad:
* **Veloway:** Para la creación y seguimiento de los envíos de los pedidos.
* **Argy-reviews:** Para publicar automáticamente una reseña cuando un usuario personaliza una nueva prenda.

#### **Proceso de Compra Completo**
El usuario puede gestionar todo el ciclo de compra: seleccionar o agregar domicilios de entrega, ingresar datos de pago y confirmar el pedido, que se registra en la base de datos junto con su número de seguimiento.

# Persicuf - Plataforma E-commerce de Personalización de Ropa

![Persicuf Home](https://i.imgur.com/k6tP4Lh.png)

Persicuf es un proyecto full-stack que simula una plataforma de e-commerce completa, permitiendo a los usuarios registrarse, personalizar prendas de vestir, gestionar sus pedidos y realizar compras. El sistema está compuesto por un backend robusto desarrollado en .NET y un frontend moderno y reactivo construido con React.

Este repositorio documenta la arquitectura, las tecnologías y las funcionalidades clave del proyecto, desarrollado como parte de un trabajo académico en equipo.

## Arquitectura y Tecnologías

El proyecto sigue una arquitectura de software moderna, desacoplando el backend y el frontend para lograr una mayor escalabilidad y mantenibilidad.

### **Backend**

[cite_start]El backend es una API RESTful construida con C# y .NET, siguiendo una arquitectura de capas bien definida. [cite: 12]

* **Framework:** .NET / ASP.NET Core
* **Lenguaje:** C#
* [cite_start]**Base de Datos:** PostgreSQL [cite: 14]
* [cite_start]**ORM:** Entity Framework, utilizando la metodología **Code First** para generar el esquema de la base de datos a partir de los modelos de C#. [cite: 13, 21]
* **Arquitectura:**
    * [cite_start]**Capas de Servicios e Interfaces:** Encapsulan toda la lógica de negocio, promoviendo un código modular y testeable. [cite: 28, 29]
    * [cite_start]**Inyección de Dependencias:** Utilizada para desacoplar los componentes de la aplicación. [cite: 22]
    * [cite_start]**DTOs (Data Transfer Objects):** Para controlar con precisión los datos expuestos por la API y optimizar la comunicación con el frontend. [cite: 17]
* **Librerías Clave:**
    * [cite_start]**Mapster:** Para el mapeo eficiente entre modelos de entidad y DTOs. [cite: 25]
    * [cite_start]**BCrypt.NET:** Para el hashing seguro de las contraseñas de los usuarios. [cite: 26]

### **Frontend**

[cite_start]El frontend es una Single-Page Application (SPA) que ofrece una experiencia de usuario fluida y dinámica. [cite: 47]

* **Librería Principal:** React
* [cite_start]**Entorno de Desarrollo:** Vite, para un desarrollo y compilación ultrarrápidos. [cite: 47]
* [cite_start]**Gestión de Formularios:** React Hook Form y Yup para la creación y validación de formularios complejos como el de registro. [cite: 48]
* [cite_start]**Componentes de UI:** Carruseles dinámicos con `slick-carousel` y alertas de navegación con `react-bootstrap`. [cite: 50, 112]
* [cite_start]**Lógica de Componentes:** Uso extensivo de Hooks de React (`useState`, `useEffect`, `useContext`) y creación de Custom Hooks (ej. `usePersonalizacionRemeras`) para encapsular y reutilizar lógica compleja. [cite: 53, 57]

## Características Destacadas

El proyecto implementa una serie de funcionalidades avanzadas que demuestran un enfoque integral en el desarrollo de software.

#### **Seguridad con JWT (JSON Web Tokens)**
Se implementó un sistema de autenticación y autorización completo basado en JWT. [cite_start]El backend genera tokens firmados con una clave secreta (HMAC SHA-256) que incluyen los `claims` del usuario (nombre, rol, etc.)[cite: 91, 92, 93]. [cite_start]La API protege endpoints específicos, permitiendo, por ejemplo, que solo usuarios con el rol de "Admin" puedan ejecutar ciertas acciones. [cite: 96] [cite_start]En el frontend, se valida la expiración del token para gestionar la sesión del usuario de forma segura. [cite: 97, 102]

#### **Cálculo de Precios Dual**
[cite_start]Para ofrecer una experiencia de usuario instantánea, el precio de las prendas personalizadas se calcula en tiempo real en el **frontend**. [cite: 107] [cite_start]Sin embargo, para garantizar la integridad y evitar manipulaciones, el cálculo final y definitivo siempre se realiza y se valida en el **backend** antes de procesar un pedido. [cite: 106, 108, 109]

#### **Integración con APIs Externas**
La plataforma se integra con servicios de terceros para extender su funcionalidad:
* [cite_start]**Veloway:** Para la creación y seguimiento de los envíos de los pedidos. [cite: 84, 85]
* [cite_start]**Argy-reviews:** Para publicar automáticamente una reseña cuando un usuario personaliza una nueva prenda. [cite: 63, 68]

#### **Proceso de Compra Completo**
[cite_start]El usuario puede gestionar todo el ciclo de compra: seleccionar o agregar domicilios de entrega, ingresar datos de pago y confirmar el pedido, que se registra en la base de datos junto con su número de seguimiento. [cite: 69, 72, 83]

---

**Autores:**
* Juan Cruz Godoy
* [Nombre de tus compañeros]

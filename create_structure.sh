#!/bin/bash

# Script para generar la estructura de carpetas y archivos para el proyecto RuedaYPatas

echo "🚀 Creando estructura de carpetas..."

# Crear directorios principales usando -p para crear subdirectorios si no existen
mkdir -p Constants
mkdir -p Data/Migrations
mkdir -p docs
mkdir -p Filters
mkdir -p Services
mkdir -p DTOs

# Crear directorios para las Vistas de cada entidad
mkdir -p Views/Mascotas
mkdir -p Views/Hospedajes
mkdir -p Views/Transportes
mkdir -p Views/Reservas
mkdir -p Views/Solicitudes
mkdir -p Views/Razas
mkdir -p Views/Ubicaciones

# Crear directorios para subida de archivos
mkdir -p wwwroot/uploads/mascotas
mkdir -p wwwroot/uploads/hospedajes

echo "✅ Carpetas creadas."

echo "📝 Creando archivos placeholder..."

# Archivos de configuración y constantes
touch Constants/Roles.cs
touch Constants/FileSettings.cs

# Archivos de la capa de Datos
touch Data/SeedData.cs

# Archivos de documentación
touch docs/database_diagram.png
touch docs/jira_user_stories_export.csv

# Archivos de filtros
touch Filters/CustomExceptionFilter.cs

# Archivos de Modelos (Entidades)
touch Models/ApplicationUser.cs
touch Models/Mascota.cs
touch Models/Hospedaje.cs
touch Models/Transporte.cs
touch Models/Raza.cs
touch Models/Ubicacion.cs
touch Models/Reserva.cs
touch Models/Solicitud.cs

# Archivos de Servicios (Lógica de negocio y API)
touch Services/IImageService.cs
touch Services/LocalImageService.cs
touch Services/IPetfinderService.cs
touch Services/PetfinderService.cs

# Archivos DTOs (Data Transfer Objects)
touch DTOs/MascotaDTO.cs
touch DTOs/HospedajeDTO.cs
touch DTOs/TransporteDTO.cs
touch DTOs/ReservaDTO.cs
touch DTOs/RazaDTO.cs

# Vistas y archivos compartidos
touch Views/Shared/_AlertPartial.cshtml

# Archivos raíz del proyecto
touch docker-compose.yml
touch README.md

echo "✅ Archivos creados."

echo "⚙️ Generando un .gitignore estándar para .NET..."

# Crear un archivo .gitignore con contenido útil para proyectos .NET
cat <<EOF > .gitignore
# Personalizaciones
.DS_Store

# Dependencias
/node_modules
/packages

# Archivos de compilación y temporales
/[Bb]in/
/[Oo]bj/

# User-specific files
*.suo
*.user
*.userosscache
*.sln.docstates

# Archivos de VS Code
.vscode

# Archivos generados por Rider
.idea/

# Archivos de publicación
/publish/

# Variables de entorno
.env

# Secretos de usuario
secrets.json

# Registros de EF
efpt.config.json

# Archivos generados
*.generated.cs
EOF

echo "✅ .gitignore creado."
echo "✨ ¡Estructura del proyecto lista! ✨"

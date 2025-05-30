# DecoHouse3D

**DecoHouse3D** es un simulador interactivo en primera persona que permite recorrer un departamento y personalizar visualmente sus elementos (paredes, muebles, etc.) mediante selección de materiales, colores y modelos 3D. Fue desarrollado en Unity y es compatible tanto con PC como con dispositivos móviles.

## 🎯 Objetivo del Proyecto

Ofrecer una experiencia inmersiva de personalización de espacios en tiempo real, con buena performance y una interfaz clara para el usuario.

## 🚀 Características

- Navegación estilo FPS (primera persona) con libertad total de movimiento.
- Interacción con objetos mediante puntero y clic/tap.
- Menú contextual para cambiar materiales o colores (5 opciones).
- Audio ambiental y efectos de sonido al interactuar.
- UI responsiva para PC y dispositivos móviles.
- Optimización con modelos low poly, texturas comprimidas y luces bakeadas.

## 📌 Organización del Trabajo

Se trabajó con metodología **Kanban** usando un [tablero de Trello](https://trello.com/b/ZrtGbOWE/decohouse-3d) para dividir tareas y gestionar los avances.

El proyecto está disponible públicamente en este repositorio:  
🔗 [GitHub - DecoHouse3D](https://github.com/manfredcamacho/DecoHouse3D)

## 🛠 Herramientas Utilizadas

- Unity 2022.3 (LTS)
- ProBuilder
- Blender (ajuste de modelos)
- Audacity (edición de audio)
- Photoshop / GIMP (UI y texturas)
- Trello (gestión de tareas)
- Git y GitHub (control de versiones)

## ⚠️ Desafíos Técnicos

- **Meshes descargados**: muchos tenían errores en el mapeo UV o escalas incompatibles. Se filtraron y reemplazaron los problemáticos.
- **Sistema de outline**: funcionaba en PC, pero daba errores en móvil. Se reemplazó por un efecto visual más simple y estable.
- **Rendimiento móvil**: se optimizó usando materiales simples, iluminación bakeada y evitando luces dinámicas.
- **UI responsiva**: se adaptó para pantallas pequeñas usando Canvas Scaler y layouts flexibles.

## 📱 Plataformas Soportadas

- Windows PC
- Android (APK disponible en releases)

---

© 2025 - Proyecto académico desarrollado para la semana del 26 al 30 de mayo.

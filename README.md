-----

# Atmos2D: A Lightweight C\# 2D Game Engine for Software Design Mastery

-----

## 🚀 Project Overview

**Atmos2D** is a lightweight, open-source 2D game engine crafted in C\# with a primary focus on demonstrating robust software design principles and modern architectural patterns. Its core objective is to serve as an exemplary portfolio piece for C\# developers, showcasing a deep understanding of game development fundamentals, object-oriented programming, and the Entity Component System (ECS) paradigm.

This engine is designed for ease of distribution and minimal setup, making it ideal for rapid prototyping of simple 2D games while providing a clear, didactic codebase for aspiring game developers and C\# enthusiasts.

**Important Note on Visuals:**
Atmos2D's current focus is on demonstrating its **architectural robustness and underlying game logic**, rather than advanced graphical rendering. Therefore, all visual elements within the engine and the demo project are intentionally represented by **text and basic geometric shapes (rectangles)**. This design choice highlights the engine's core functionality and its ability to manage game state and interactions independently of complex textures or assets. It emphasizes the engine's readiness for future integration with more sophisticated rendering pipelines.

## ✨ Key Features (Minimum Viable Product - MVP)

Atmos2D currently provides the essential building blocks for 2D game development:

  * **Window and Game Loop Management:** Robust control over the application window and a stable `Update`/`Draw` game loop.
  * **Modular Design:** Built with modularity in mind, allowing for easy extension and integration of new features or systems.
  * **User Input Handling:** A streamlined API for processing keyboard and mouse inputs.
  * **Entity Component System (ECS):** A functional ECS implementation enabling flexible and scalable game object composition and logic.
  * **Basic Collision Detection:** Efficiently handles collisions between entities, demonstrating fundamental game physics principles.
  * **Entity Lifecycle Management:** Robust mechanisms for creating, updating, and destroying entities, ensuring clean resource management.
  * **C# Best Practices:** Adheres to industry-standard C# coding conventions and design patterns, reflecting a commitment to high-quality code.
  * **Raylib Integration:** Leverages the power and simplicity of Raylib for low-level graphics rendering, demonstrating proficiency in integrating third-party libraries.


## 💡 Technical Highlights & Why This Project?

Atmos2D stands out as a technical demonstration due to its adherence to best practices:

  * **Architectural Excellence:** Built upon a **modular and layered architecture** that strictly prevents circular dependencies, promoting maintainability and scalability.
  * **Design Pattern Showcase:** A strong emphasis on common and advanced **design patterns**, most notably a clean implementation of the **Entity Component System (ECS)**, complemented by patterns like Factory, Strategy, and Observer.
  * **C\# Best Practices:** Adherence to standard C\# naming conventions, proper resource disposal (`IDisposable`), and clean, self-documenting code.
  * **Lightweight & Portable:** By leveraging a low-level graphics library (like Raylib-cs), Atmos2D ensures minimal external dependencies, resulting in a small footprint and straightforward deployment.
  * **Portfolio Ready:** This project is meticulously crafted to serve as an exceptional entry for a **junior developer's portfolio**, demonstrating not just coding ability but a profound understanding of complex software engineering challenges in game development.

## 🛠️ Technologies Used

  * **C\#:** The primary programming language.
  * **.NET 8.0:** The modern cross-platform framework providing a robust runtime environment.
  * **Raylib-cs:** A C\# wrapper for Raylib, a simple and easy-to-use library for low-level graphics programming, ensuring performance and minimal overhead.

## 🚀 Getting Started

Follow these steps to get Atmos2D up and running on your local machine:

1.  **Clone the Repository:**
    ```bash
    git clone https://github.com/LouisJoly/Atmos2D.git
    cd Atmos2D
    ```
2.  **Open the Solution:**
    Open the `Atmos2D.sln` file in Visual Studio (2022 or later) or your preferred .NET IDE.
3.  **Restore NuGet Packages:**
    Visual Studio should automatically restore the necessary NuGet packages. If not, run:
    ```bash
    dotnet restore
    ```
4.  **Build the Solution:**
    Build the entire solution to compile all projects:
    ```bash
    dotnet build
    ```
5.  **Run the Example Project:**
    Set `Atmos2D.GameExample` as the startup project and run it.
    ```bash
    dotnet run --project Atmos2D.GameExample
    ```
    This will launch a simple demo application showcasing the engine's current capabilities.

## 📂 **Architecture and Design Philosophy**

Atmos2D is built upon several core design principles:

### **Entity-Component-System (ECS)**

The ECS paradigm is central to Atmos2D. This architecture promotes:
* **Composition over Inheritance:** Entities are simple IDs, and their behavior is defined by attaching components (data) and systems (logic).
* **Data-Oriented Design:** Optimizes data access patterns for performance by grouping similar data.
* **Flexibility and Reusability:** Components and systems can be easily reused across different entities and games.

This implementation showcases a strong grasp of how to design scalable and maintainable game architectures.

### **Clean Code & Modularity**

Emphasis has been placed on writing **clean, readable, and well-documented code**. The project structure is highly modular, with clear separation of concerns, which makes it easy for other developers to understand, contribute to, and extend the engine. This reflects a commitment to software engineering best practices.


This layered structure ensures clear separation of concerns and facilitates independent development and testing of each module.

## 🤝 Contributing

While Atmos2D is primarily a portfolio project, contributions are always welcome\! If you have ideas for improvements, bug fixes, or new features, feel free to:

1.  Fork the repository.
2.  Create a new branch for your feature (`git checkout -b feature/AmazingFeature`).
3.  Commit your changes (`git commit -m 'Add some AmazingFeature'`).
4.  Push to the branch (`git push origin feature/AmazingFeature`).
5.  Open a Pull Request.

## 📄 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## 👤 Author

Louis Joly - [Link to my GitHub Profile](https://github.com/LouisJoly) / [Link to my LinkedIn Profile](https://www.linkedin.com/in/louis-joly-07/)

-----
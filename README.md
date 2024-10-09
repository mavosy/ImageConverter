
<a id="readme-top"></a>

<!-- PROJECT SHIELDS -->
[![Contributors][contributors-shield]][contributors-url]
[![Forks][forks-shield]][forks-url]
[![Stargazers][stars-shield]][stars-url]
[![Issues][issues-shield]][issues-url]
[![MIT License][license-shield]][license-url]
[![LinkedIn][linkedin-shield]][linkedin-url]

<!-- PROJECT LOGO -->
<br />
<div align="center">
<!--  <a href="https://github.com/mavosy/ImageConverter">
    <img src="images/logo.png" alt="Logo" width="80" height="80">
  </a> -->

<h3 align="center">ImageConverter</h3>

  <p align="center">
    A WPF application that converts images to grayscale.
    <br />
    <a href="https://github.com/mavosy/ImageConverter"><strong>Explore the docs »</strong></a>
    <br />
    <br />
    <a href="https://github.com/mavosy/ImageConverter/issues/new?labels=bug&template=bug-report---.md">Report Bug</a>
    ·
    <a href="https://github.com/mavosy/ImageConverter/issues/new?labels=enhancement&template=feature-request---.md">Request Feature</a>
  </p>
</div>



<!-- TABLE OF CONTENTS -->
<details>
  <summary>Table of Contents</summary>
  <ol>
    <li>
      <a href="#about-the-project">About The Project</a>
      <ul>
        <li><a href="#built-with">Built With</a></li>
      </ul>
    </li>
    <li>
      <a href="#getting-started">Getting Started</a>
      <ul>
        <li><a href="#prerequisites">Prerequisites</a></li>
        <li><a href="#installation">Installation</a></li>
      </ul>
    </li>
    <li><a href="#usage">Usage</a></li>
    <li><a href="#roadmap">Roadmap</a></li>
    <li><a href="#license">License</a></li>
    <li><a href="#contact">Contact</a></li>
  </ol>
</details>



<!-- ABOUT THE PROJECT -->
## About The Project
<div align="center">

![product_image][product-screenshot]
</div>

**ImageConverter** is a WPF application built using the MVVM design pattern. It allows users to upload an image, convert it to grayscale, and save the result. Users can choose between sequential and parallel processing modes:

- **Sequential Processing**: Uses pointers for performance.
- **Parallel Processing**: Parallel execution to utilize multiple cores.

<p align="right">(<a href="#readme-top">back to top</a>)</p>



### Built With

* [![C#][csharp-shield]][csharp-url]
* [![WPF][wpf-shield]][wpf-url]
* MVVM Pattern

<p align="right">(<a href="#readme-top">back to top</a>)</p>



<!-- GETTING STARTED -->
## Getting Started

Follow these steps to get a local copy up and running.

### Prerequisites

- .NET 6.0 SDK or later
- Visual Studio 2022 or another IDE with WPF support

### Installation

1. Clone the repo.

2. Open the solution in Visual Studio and build the project.

<p align="right">(<a href="#readme-top">back to top</a>)</p>

<!-- USAGE EXAMPLES -->
## Usage

1. Launch the application.
2. Upload an image file using the provided UI.
3. Select the processing mode, sequential or paralell
4. Process the image to convert it to grayscale.
5. Save the converted image to your local storage.

<p align="right">(<a href="#readme-top">back to top</a>)</p>



<!-- ROADMAP -->
## Roadmap

- [ ] Improve the user interface for a more modern look.
- [ ] Add unit tests.
- [ ] Implement better benchmarking tools to measure processing times.
- [ ] Support additional image formats.
- [ ] Enable bulk image processing.
- [ ] Enable block processing/image tiling for paralell processing.

See the [open issues](https://github.com/mavosy/ImageConverter/issues) for a full list of proposed features (and known issues).

<p align="right">(<a href="#readme-top">back to top</a>)</p>



<!-- CONTRIBUTING -->
## Contributing

Contributions are what make the open source community such an amazing place to learn, inspire, and create. Any contributions you make are **greatly appreciated**.

If you have a suggestion that would make this better, please fork the repo and create a pull request. You can also simply open an issue with the tag "enhancement".
Don't forget to give the project a star! Thanks again!

1. Fork the Project
2. Create your Feature Branch (`git checkout -b feature/AmazingFeature`)
3. Commit your Changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the Branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

<p align="right">(<a href="#readme-top">back to top</a>)</p>

### Top contributors:

<a href="https://github.com/mavosy/ImageConverter/graphs/contributors">
  <img src="https://contrib.rocks/image?repo=mavosy/ImageConverter" alt="contrib.rocks image" />
</a>



<!-- LICENSE -->
## License

Distributed under the MIT License. See `LICENSE.txt` for more information.

<p align="right">(<a href="#readme-top">back to top</a>)</p>



<!-- CONTACT -->
## Contact

mavosy - maltesydow@gmail.com

Project Link: [https://github.com/mavosy/ImageConverter](https://github.com/mavosy/ImageConverter)

<p align="right">(<a href="#readme-top">back to top</a>)</p>


<!-- MARKDOWN LINKS & IMAGES -->
<!-- https://www.markdownguide.org/basic-syntax/#reference-style-links -->
[contributors-shield]: https://img.shields.io/github/contributors/mavosy/ImageConverter.svg?style=for-the-badge
[contributors-url]: https://github.com/mavosy/ImageConverter/graphs/contributors
[forks-shield]: https://img.shields.io/github/forks/mavosy/ImageConverter.svg?style=for-the-badge
[forks-url]: https://github.com/mavosy/ImageConverter/network/members
[stars-shield]: https://img.shields.io/github/stars/mavosy/ImageConverter.svg?style=for-the-badge
[stars-url]: https://github.com/mavosy/ImageConverter/stargazers
[issues-shield]: https://img.shields.io/github/issues/mavosy/ImageConverter.svg?style=for-the-badge
[issues-url]: https://github.com/mavosy/ImageConverter/issues
[license-shield]: https://img.shields.io/github/license/mavosy/ImageConverter.svg?style=for-the-badge
[license-url]: https://github.com/mavosy/ImageConverter/blob/master/LICENSE.txt
[linkedin-shield]: https://img.shields.io/badge/-LinkedIn-black.svg?style=for-the-badge&logo=linkedin&colorB=555
[linkedin-url]: https://linkedin.com/in/malte-von-sydow
[product-screenshot]: Images/ITGscreenshot.png
[csharp-shield]: https://custom-icon-badges.demolab.com/badge/C%23-%23239120.svg?logo=cshrp&logoColor=white
[csharp-url]: https://learn.microsoft.com/en-us/dotnet/csharp/
[wpf-shield]: https://img.shields.io/badge/WPF-512BD4?style=for-the-badge&logo=windows&logoColor=white
[wpf-url]: https://learn.microsoft.com/en-us/dotnet/desktop/wpf/

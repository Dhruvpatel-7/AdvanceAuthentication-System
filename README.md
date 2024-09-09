# AdvanceLogin
<!-- Improved compatibility of back to top link: See: https://github.com/othneildrew/Best-README-Template/pull/73 -->
<a id="readme-top"></a>

<!-- PROJECT LOGO -->
<br />
<div align="center">
  <a href="https://github.com/othneildrew/Best-README-Template">
    <img src="images/logo.png" alt="Logo" width="80" height="80">
  </a>

  <h3 align="center">Advance Login</h3>

  <p align="center">
    An awesome Login and register With Jwt ,Passwordhash, Refreshtoken Authentication Authorization.
    <br />
    <a href="https://github.com/othneildrew/Best-README-Template"><strong>Explore the docs »</strong></a>
    <br />
  </p>
</div>

### Built With

<p align="left"> <a href="https://angular.io" target="_blank" rel="noreferrer"> <img src="https://angular.io/assets/images/logos/angular/angular.svg" alt="angular" width="40" height="40"/> </a> <a href="https://getbootstrap.com" target="_blank" rel="noreferrer"> <img src="https://raw.githubusercontent.com/devicons/devicon/master/icons/bootstrap/bootstrap-plain-wordmark.svg" alt="bootstrap" width="40" height="40"/> </a> <a href="https://www.w3schools.com/cs/" target="_blank" rel="noreferrer"> <img src="https://raw.githubusercontent.com/devicons/devicon/master/icons/csharp/csharp-original.svg" alt="csharp" width="40" height="40"/> </a> <a href="https://www.w3schools.com/css/" target="_blank" rel="noreferrer"> <img src="https://raw.githubusercontent.com/devicons/devicon/master/icons/css3/css3-original-wordmark.svg" alt="css3" width="40" height="40"/> </a> </a> <a href="https://dotnet.microsoft.com/" target="_blank" rel="noreferrer"> <img src="https://raw.githubusercontent.com/devicons/devicon/master/icons/dot-net/dot-net-original-wordmark.svg" alt="dotnet" width="40" height="40"/> </a> <a href="https://git-scm.com/" target="_blank" rel="noreferrer"> <img src="https://www.vectorlogo.zone/logos/git-scm/git-scm-icon.svg" alt="git" width="40" height="40"/> </a> <a href="https://www.w3.org/html/" target="_blank" rel="noreferrer"> <img src="https://raw.githubusercontent.com/devicons/devicon/master/icons/html5/html5-original-wordmark.svg" alt="html5" width="40" height="40"/> </a> <a href="https://developer.mozilla.org/en-US/docs/Web/JavaScript" target="_blank" rel="noreferrer"> <img src="https://raw.githubusercontent.com/devicons/devicon/master/icons/javascript/javascript-original.svg" alt="javascript" width="40" height="40"/> </a> <a href="https://karma-runner.github.io/latest/index.html" target="_blank" rel="noreferrer"> <img src="https://raw.githubusercontent.com/detain/svg-logos/780f25886640cef088af994181646db2f6b1a3f8/svg/karma.svg" alt="karma" width="40" height="40"/> </a> <a href="https://www.microsoft.com/en-us/sql-server" target="_blank" rel="noreferrer">   </a> <a href="https://postman.com" target="_blank" rel="noreferrer"> <img src="https://www.vectorlogo.zone/logos/getpostman/getpostman-icon.svg" alt="postman" width="40" height="40"/> </a> <a href="https://www.python.org" target="_blank" rel="noreferrer"> </a> <a href="https://www.typescriptlang.org/" target="_blank" rel="noreferrer"> <img src="https://www.svgrepo.com/show/303229/microsoft-sql-server-logo.svg" alt="mssql" width="40" height="40"/> </a> <a href="https://www.mysql.com/" target="_blank" rel="noreferrer"> <img src="https://raw.githubusercontent.com/devicons/devicon/master/icons/typescript/typescript-original.svg" alt="typescript" width="40" height="40"/> </a> </p>

<!-- ABOUT THE PROJECT -->
## About The Project
## Overview

This project is an authentication system designed to manage user registration, login, and secure sessions using JSON Web Tokens (JWT). It features mechanisms for password hashing and token refreshing to enhance security and user experience.

## Key Features

### 1. User Registration
- **Purpose**: Allows new users to create an account.
- **Process**:
  - Users provide their credentials (e.g., username, email, and password).
  - Passwords are hashed using a secure hashing algorithm (e.g., bcrypt) before storage.
  - User data, including the hashed password, is saved in a database.

### 2. User Login
- **Purpose**: Authenticates users and issues access tokens.
- **Process**:
  - Users submit their credentials (username/email and password).
  - The provided password is hashed and compared with the stored hashed password in the database.
  - Upon successful authentication, an access token (JWT) is generated and returned.

### 3. JWT (JSON Web Token)
- **Purpose**: Provides a secure way to transmit information between parties.
- **Usage**:
  - After successful login, a JWT containing user information and an expiration time is generated.
  - The token is signed with a secret key to ensure its integrity and authenticity.
  - The token is included in the `Authorization` header of subsequent requests to access protected routes.

### 4. Refresh Token
- **Purpose**: Allows users to obtain a new access token without re-authenticating.
- **Usage**:
  - Alongside the access token, a refresh token is issued.
  - When the access token expires, the refresh token can be used to request a new access token.
  - Refresh tokens have a longer expiration time and are stored securely on the client-side.

### 5. Password Hashing
- **Purpose**: Enhances security by protecting user passwords.
- **Process**:
  - Passwords are hashed using a strong algorithm (e.g., bcrypt) before storage.
  - During login, the entered password is hashed and compared with the stored hashed password.

## Security Considerations
- **Hashing Algorithm**: Use a strong and well-established hashing algorithm like bcrypt or Argon2.
- **JWT Secret**: Ensure the JWT secret key is kept secure.
- **Token Expiry**: Implement appropriate expiration times for both access and refresh tokens.
- **Secure Storage**: Store refresh tokens securely on the client side (e.g., in HTTP-only cookies).

## Screenshots

Here is a screenshot of the application:

![Application Screenshot](images/screenshot.png)


## License

Distributed under the MIT License. See `LICENSE.txt` for more information.

<p align="right">(<a href="#readme-top">back to top</a>)</p>

## Contact

Your Name- Dhruv Patel - dnp1982002@gmail.com

Project Link: https://github.com/Dhruvpatel-7/AdvanceLogin

<p align="right">(<a href="#readme-top">back to top</a>)</p>



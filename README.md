# PokeJournal-API

[![Codacy Badge](https://app.codacy.com/project/badge/Grade/8e16eaa996374115900d7f0bb5ce40c3)](https://app.codacy.com/gh/lu-css/PokeJournal-API/dashboard?utm_source=gh&utm_medium=referral&utm_content=&utm_campaign=Badge_grade)

Api para aplicação [Poke Journal](https://github.com/lu-css/Pokejournal/).

URL para acesso `https://pokejournal-api.onrender.com/`

<div align="center">
<img src="img/Pokemon.png" height="250px">
</div>

### 3°A Etim - Desenvolvimento de Sistemas
> Débora Liberato Ribeiro (づ｡◕‿‿◕｡)づ<br>
> Lucas dos Santos Silva ⊂◉‿◉つ

## Descrição das atualizações de informação no aplicativo PokéJournal (API)

As novas funcionalidades são “Favoritar” e “Adicionar equipe” ambos relacionados com a criação da nova API, o botão para primeira função citada já foi criado anteriormente, porém não apresentava funcionamento. Nossos planos agora para a API são criar um EndPoint que salvará um Pokémon como favorito. Para a criação das equipes outro EndPoint  permitirá a adição de até cinco Pokémons.

## Diagramas

## Diagrama de classes

<img src="img/classDiagram.jpg" alt="class diagram" />

## DER

<img src="img/api-der.png" alt="der" width="50%" />

<img src="img/der-certo.jpg" alt="der" />

## Run

### Dependências

  - Docker Composer
  - dotnet-ef

### Instalar

  - dotnet-ef

```sh
dotnet tool install --global dotnet-ef
```

### Config

Veja o arquivo `.env.example` para instruções de variáveis de ambiente.

> **Apenas o campo `JWY_SECRET_KEY` é obrigatório!**. Para teste, pode-se usar o valor `027c96e076aee5de1678be058f8c26d71732f26b`.

### Rodar

  - Clone Project
```
git clone https://github.com/lu-css/PokeJournal-API
```

  - cd para diretório

```sh
cd PokeJournal-API/app
```

  - Instalar Dependencias

```sh
dotnet restore
```

  - Iniciar MySQL no docker-composer

```sh
docker compose up -d
```

  - Rodar Migrations

```sh
dotnet ef database update
```

  - Rodar
```sh
dotnet run
```
## Funcionamento

https://github.com/lu-css/PokeJournal-API/assets/97306254/24ef6b02-8580-45ca-82fd-ba00f44f1fa1

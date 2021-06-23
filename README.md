# Teste Altudo
> Create an application that would accept a web URL and display all of the images used on the page and create a graph with the top 10 used words. 
> There is no requirement on the technology or tools to be used for that; this part is up to you 
> Feel free to ask me (the person conducting the first interview and sending this email) whatever and however many questions over email to help you create the solution 
> Once the solution is created, please share the source code and the installation package with instructions on using it.  

Para este desafio em questão foi optado por desenvolver uma aplicação web c# asp .net core MVC, utilizando o Visual Studio 2019 como ferramenta de desenvolvimento.

Em uma interface simples e prática, o usuário possui um formulário para informar uma URL, pesquisar e o retorno da pesquisa foi dividido em duas partes: imagens do site pesquisado e um gráfico de pizza com as 10 palavras mais utilizadas no mesmo.

### Features
- [x] Pesquisa por URL
- [x] Listar imagens do site pesquisado
- [x] Criar gráfico com as 10 palavras mais utilizadas no site

#### Pesquisa por URL
- Usuário pode colocar URLs completa ou apenas o domínio. 
- Para análise dos dados foi utilizado uma biblioteca open source (**HtmlAgilityPack**), que possibilita fazer uma requisição em paralelo via backend e trabalhar com o retorno utilizando os nodes do html
- Realizado tratamento, para desconsiderar tags como: [style, script, link, noscript] que acabam influenciando no resultado dos demais processos (imagens e palavras mais usadas)

#### Listar imagens do site pesquisado
- Para análise dos dados foi utilizado uma biblioteca open source (**HtmlAgilityPack**), que possibilita fazer uma requisição em paralelo via backend e trabalhar com o retorno utilizando os nodes do html
- Foram apresentados apenas imagens que constam na TAG \<img\> dentro do \<body\> da página

#### Criar gráfico com as 10 palavras mais utilizadas no site
- Para análise dos dados, foi utilizado uma biblioteca open source (**HtmlAgilityPack**), que possibilita fazer uma requisição em paralelo via backend e trabalhar com o retorno utilizando os nodes do html
- Para identificar as palavas mais usadas, foram levados em consideração:
  1. Apenas textos dentro da TAG \<body\> (text())
  2. Realizado separação de palavras por meio dos caracteres **[' ', '.', ',', '\'', '-', '_', '"', '(', ')', '{', '}', '[', ']', '^']**
  3. Considerado neste teste como "palavras mais usadas", apenas palavras onde possuiam 3 ou mais caracteres, sendo um parâmetro o número de caracteres para definição de "palavra"
  4. Para apresentar o gráfico das palavras mais usadas, foi uilizado uma biblioteca js open souce (**ChartJs**)

<template>
 <div class="container">
    <router-view></router-view>
  </div>
</template>


<style lang="scss">

@font-face {
	font-display: swap;
	font-family: 'Nunito';
	font-style: normal;
	font-weight: 400;
	src: url('assets/fonts/Nunito/Nunito-Regular.ttf') format('truetype');
}

@font-face {
	font-display: swap;
	font-family: 'Roboto';
	font-style: normal;
	font-weight: 100;
	src: url('assets/fonts/Roboto/Roboto-Thin.eot') format('embedded-opentype');
	src: url('assets/fonts/Roboto/Roboto-Thin.ttf') format('truetype');
	src: url('assets/fonts/Roboto/Roboto-Thin.woff') format('woff');
	src: url('assets/fonts/Roboto/Roboto-Thin.woff2') format('woff2');
}
.title {
    font-size: 1.5rem;
    font-weight: bold;
    margin-bottom: 1rem;
    text-transform: uppercase;
    font-family: nunito;
}
    .dropdown, .input-text {
        display: flex;
        width: 300px;   
    }

    
    .links {
        color: #253E8B;
        display: flex;
        overflow: hidden;
        width: 20rem;
        text-align: center;
        flex-direction: column;
        align-items: center;
        justify-content: center;
    }

    .links:hover {
        color: #fab03f;
    }

   
</style>

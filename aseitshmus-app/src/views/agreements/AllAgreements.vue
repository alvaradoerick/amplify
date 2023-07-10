<script setup>
    import {
        useStore
    } from 'vuex'
    import {
        computed,
        onMounted,
        ref,
        watch
    } from 'vue';

    import TabView from 'primevue/tabview';
    import TabPanel from 'primevue/tabpanel';
    import Card from 'primevue/card';


    const {
        dispatch,
        getters
    } = useStore();
    
    const ALL = {
        title: 'Mostrar todos',
        id: 0
    }

    const categories = computed(() => getters['categories/getCategory']);
    const agreements = computed(() => getters['agreements/filteredAgreements']);
    const activeTab = ref(ALL.id);
    const moreInfo = 'Me interesa';
    const tabs = ref([ALL]);

    watch(activeTab, (value) => {
        dispatch('agreements/setActiveCategory', tabs.value[value]);
    }, {
        immediate: true
    })

    onMounted(async () => {
        await dispatch('agreements/getActiveAgreements');
        await dispatch('categories/getActiveCategories');
        categories.value.forEach((category) => {
            tabs.value.push({
                title: category.Description,
                id: category.CategoryAgreementId
            })
        });
       
    })
</script>
<template>
    <div>
        <TabView v-model:active-index="activeTab">
            <TabPanel v-for="tab in tabs" :key="tab.id" :header="tab.title">
                <div class="cards">
                    <Card class="card" v-for="agreement in agreements" :key="agreement.AgreementId" :agreement="agreement">
                    <template #title > 
                        <img class="card-image" :src="agreement.Image" :alt="agreement.Title">
                        <h2 class="card-title">
                            {{ agreement.Title}} 
                            
                        </h2>
                    </template>
                    <template #subtitle>
                        <p>
                        {{ agreement.Description}}
                        </p>
                    </template>
                    <template #content>
                       <base-button :label="moreInfo"  :type="'button'" class="button"></base-button>
                    </template>
                    </Card>
                </div>
            </TabPanel>
        </TabView>
    </div>

</template>
<style scoped>
    .cards {
        display: flex;
        flex-wrap: wrap;
        justify-content: space-between;
        overflow: none;
        height: 100%;
    }
    
    .button {
        display: flex;
        background-color: #253e8b;
        border-color: #253e8b;
        overflow: hidden;
        width: 10rem;
        color: white;
        text-align: center;
        flex-direction: row;
        align-items: center;
        justify-content: center;
    }


    .card {
        display: flex;
        width: 23rem;
        flex-direction: column;
        margin-bottom: 2rem;
        margin-right: 2rem;
    }
    
    .card-image {
        width: 100%;
        height: 8rem;
        margin-bottom: 3rem;
    }
    
    .card-title {
        font-size: 1rem;
        font-weight: bold;
        margin: 0.5rem 0;
        text-transform: uppercase;
    }
</style>
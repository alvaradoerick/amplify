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
    
    // import {
    //         useToast
    //     } from 'primevue/usetoast';

    import TabView from 'primevue/tabview';
    import TabPanel from 'primevue/tabpanel';
    import Card from 'primevue/card';


    const {
        dispatch,
        getters
    } = useStore();
    //const toast = useToast();

    const categories = computed(() => getters['categories/getCategory']);
    const agreements = computed(() => getters['agreements/filteredAgreements']);
    const activeTab = ref(0);

    const tabs = ref([{
        title: 'Mostrar todos',
        id: 0
    }, ]);

    //methods
    watch(activeTab, (value) => {
        if (value) {
            console.log(value)
            dispatch('agreements/setActiveCategory', value)
        }
    })



    onMounted(async () => {
        await dispatch('agreements/getActiveAgreements');
        await dispatch('categories/getActiveCategories');
        categories.value.forEach((category) => {
            tabs.value.push({
                title: category.Description,
                id: category.CategoryAgreementId
            })
        })
    })
</script>


<template>

    <div class="card">
        <TabView v-model:active-index="activeTab">
            <TabPanel v-for="tab in tabs" :key="tab.id" :header="tab.title">
                <Card v-for="agreement in agreements" :key="agreement.AgreementId" :agreement="agreement">
    <template #title> {{ agreement.Title}} </template>
    <template #subtitle>
        <p>
            {{ agreement.Description}}
        </p>
    </template>
    <template #content>
      
            {{ agreement.Image}}
      
    </template>
</Card>

            </TabPanel>
        </TabView>
    </div>

</template>
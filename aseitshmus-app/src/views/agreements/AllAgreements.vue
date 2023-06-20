<script setup>

import { ref } from 'vue';

import TabView from 'primevue/tabview';
import TabPanel from 'primevue/tabpanel';






</script>


<template>

<div class="card">
        <TabView>
            <TabPanel v-for="tab in tabs" :key="tab.title" :header="tab.title">
                <p>{{ tab.content }}</p>
            </TabPanel>
        </TabView>
    </div>
    
</template>
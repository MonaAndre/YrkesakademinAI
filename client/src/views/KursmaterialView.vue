<template>
  <div class="page">
    <h1>Kursmaterial</h1>
    <p>Beskriv vilket kursmaterial du behöver hjälp med att skapa eller förklara.</p>
    <form @submit.prevent="handleSubmit">
      <label for="amne">Ämne</label>
      <input
        id="amne"
        v-model="amne"
        type="text"
        placeholder="T.ex. 'Objektorienterad programmering'"
      />
      <label for="malgrupp">Målgrupp</label>
      <input
        id="malgrupp"
        v-model="malgrupp"
        type="text"
        placeholder="T.ex. 'Nybörjare på programmet'"
      />
      <button type="submit" :disabled="loading">
        {{ loading ? 'Väntar på svar...' : 'Skicka' }}
      </button>
    </form>
    <div v-if="response" class="response">
      <h2>Svar:</h2>
      <div v-html="marked.parse(response)"></div>
    </div>
    <div v-if="error" class="error">{{ error }}</div>
  </div>
</template>

<script setup>
import { ref } from 'vue'
import { marked } from 'marked'

const amne = ref('')
const malgrupp = ref('')
const response = ref('')
const loading = ref(false)
const error = ref('')

async function handleSubmit() {
  if (!amne.value.trim() || !malgrupp.value.trim()) return
  loading.value = true
  error.value = ''
  response.value = ''
  try {
    const res = await fetch('/api/kursmaterial', {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify({ amne: amne.value, malgrupp: malgrupp.value }),
    })
    if (!res.ok) throw new Error('Något gick fel med servern.')
    const data = await res.json()
    response.value = data.reply
  } catch (e) {
    error.value = e.message
  } finally {
    loading.value = false
  }
}
</script>

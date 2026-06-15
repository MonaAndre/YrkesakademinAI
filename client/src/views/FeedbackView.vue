<template>
  <div class="page">
    <h1>Feedback</h1>
    <p>Skriv in din fråga eller text nedan så svarar AI:n dig.</p>
    <form @submit.prevent="handleSubmit">
      <textarea
        v-model="input"
        placeholder="Skriv din text här..."
        rows="6"
      ></textarea>
      <button type="submit" :disabled="loading">
        {{ loading ? 'Väntar på svar...' : 'Skicka' }}
      </button>
    </form>
    <div v-if="response" class="response">
      <h2>Svar:</h2>
      <p>{{ response }}</p>
    </div>
    <div v-if="error" class="error">{{ error }}</div>
  </div>
</template>

<script setup>
import { ref } from 'vue'

const input = ref('')
const response = ref('')
const loading = ref(false)
const error = ref('')

async function handleSubmit() {
  if (!input.value.trim()) return
  loading.value = true
  error.value = ''
  response.value = ''
  try {
    const res = await fetch('/api/feedback', {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify({ message: input.value }),
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

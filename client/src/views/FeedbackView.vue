<template>
  <div class="page">
    <h1>Feedback</h1>
    <p>Skriv in din fråga eller text nedan så svarar AI:n dig.</p>
    <form @submit.prevent="handleSubmit">
      <label for="inlamning">Studentinlämning</label>
      <textarea
        id="inlamning"
        v-model="inlamning"
        placeholder="Klistra in studentens inlämning här..."
        rows="6"
      ></textarea>
      <label for="kriterier">Bedömningskriterier</label>
      <textarea
        id="kriterier"
        v-model="kriterier"
        placeholder="Skriv in bedömningskriterierna här..."
        rows="6"
      ></textarea>
      <button type="submit" :disabled="loading">
        {{ loading ? 'Väntar på svar...' : 'Skicka' }}
      </button>
    </form>
    <div v-if="response" class="response">
      <h2>Svar:</h2>
      <div v-html="marked.parse(response)"></div>

      <label for="studentEmail">Studentens mejl</label>
      <input
        id="studentEmail"
        v-model="studentEmail"
        type="email"
        placeholder="student@exempel.se"
      />
      <button type="button" :disabled="sendLoading" @click="handleSendToStudent">
        {{ sendLoading ? 'Skickar...' : 'Skicka till student' }}
      </button>

      <div v-if="sendSuccess" class="success">Mejl skickat!</div>
      <div v-if="sendError" class="error">{{ sendError }}</div>
    </div>
    <div v-if="error" class="error">{{ error }}</div>
  </div>
</template>

<script setup>
import { ref } from 'vue'
import { marked } from 'marked'

const inlamning = ref('')
const kriterier = ref('')
const response = ref('')
const loading = ref(false)
const error = ref('')

const studentEmail = ref('')
const sendLoading = ref(false)
const sendSuccess = ref(false)
const sendError = ref('')

async function handleSubmit() {
  if (!inlamning.value.trim() || !kriterier.value.trim()) return
  loading.value = true
  error.value = ''
  response.value = ''
  try {
    const res = await fetch('/api/feedback', {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify({ inlamning: inlamning.value, kriterier: kriterier.value }),
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

async function handleSendToStudent() {
  if (!studentEmail.value.trim()) return
  sendLoading.value = true
  sendSuccess.value = false
  sendError.value = ''
  try {
    const res = await fetch('http://localhost:5678/webhook/d179fa8a-acbd-43b0-9620-deebbef44941', {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify({ studentEmail: studentEmail.value, feedback: marked.parse(response.value) }),
    })
    if (!res.ok) throw new Error('Något gick fel vid sändning av mejlet.')
    sendSuccess.value = true
  } catch (e) {
    sendError.value = e.message
  } finally {
    sendLoading.value = false
  }
}
</script>

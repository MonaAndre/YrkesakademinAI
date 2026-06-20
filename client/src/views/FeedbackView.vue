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

      <div class="send-panel">
        <p class="send-panel-title">📨 Skicka feedback till student</p>
        <div class="send-row">
          <input
            id="studentEmail"
            v-model="studentEmail"
            type="email"
            placeholder="student@exempel.se"
            class="email-input"
          />
          <button
            type="button"
            class="send-btn"
            :disabled="sendLoading"
            @click="handleSendToStudent"
          >
            <span v-if="sendLoading">Skickar…</span>
            <span v-else>Skicka ✉️</span>
          </button>
        </div>
        <div v-if="sendSuccess" class="success">✅ Mejl skickat!</div>
        <div v-if="sendError" class="error">{{ sendError }}</div>
      </div>
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

<style scoped>
.send-panel {
  margin-top: 1.5rem;
  padding: 1rem 1.25rem;
  background: #f0f4ff;
  border: 1px solid #c5cae9;
  border-radius: 8px;
}

.send-panel-title {
  font-weight: 600;
  color: #1a237e;
  margin-bottom: 0.75rem;
  font-size: 0.95rem;
}

.send-row {
  display: flex;
  gap: 0.75rem;
  align-items: center;
}

.email-input {
  flex: 1;
  padding: 0.65rem 1rem;
  font-size: 0.95rem;
  border: 1.5px solid #9fa8da;
  border-radius: 6px;
  font-family: inherit;
  background: white;
  margin-bottom: 0;
  transition: border-color 0.2s;
}

.email-input:focus {
  outline: none;
  border-color: #1a237e;
}

.send-btn {
  white-space: nowrap;
  padding: 0.65rem 1.25rem;
  font-size: 0.95rem;
  background: #43a047;
  border-radius: 6px;
  transition: background 0.2s;
}

.send-btn:hover:not(:disabled) {
  background: #388e3c;
}

.send-btn:disabled {
  background: #a5d6a7;
  cursor: not-allowed;
}
</style>

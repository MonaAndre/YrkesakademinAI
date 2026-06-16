<template>
  <div id="app">
    <header>
      <h2>YrkesakademinAI</h2>
      <nav>
        <RouterLink to="/feedback">Feedback</RouterLink>
        <RouterLink to="/kursmaterial">Kursmaterial</RouterLink>
        <RouterLink to="/faq">FAQ</RouterLink>
      </nav>
    </header>
    <main>
      <RouterView />
    </main>
  </div>
</template>

<style>
* {
  box-sizing: border-box;
  margin: 0;
  padding: 0;
}

body {
  font-family: 'Segoe UI', sans-serif;
  background: #f5f5f5;
  color: #222;
}

#app {
  min-height: 100vh;
  display: flex;
  flex-direction: column;
}

header {
  background: #1a237e;
  color: white;
  padding: 1rem 2rem;
  display: flex;
  align-items: center;
  gap: 2rem;
}

header h2 {
  font-size: 1.2rem;
  font-weight: 600;
}

nav {
  display: flex;
  gap: 1.5rem;
}

nav a {
  color: white;
  text-decoration: none;
  padding: 0.4rem 0.8rem;
  border-radius: 4px;
  transition: background 0.2s;
}

nav a:hover,
nav a.router-link-active {
  background: rgba(255, 255, 255, 0.2);
}

main {
  flex: 1;
  padding: 2rem;
  max-width: 800px;
  margin: 0 auto;
  width: 100%;
}

.page h1 {
  margin-bottom: 0.5rem;
  color: #1a237e;
}

.page p {
  margin-bottom: 1rem;
  color: #555;
}

label {
  display: block;
  font-weight: 600;
  color: #1a237e;
  margin-bottom: 0.4rem;
}

textarea,
input[type='text'] {
  width: 100%;
  padding: 0.75rem;
  font-size: 1rem;
  border: 1px solid #ccc;
  border-radius: 6px;
  resize: vertical;
  margin-bottom: 1rem;
  font-family: inherit;
}

textarea:focus,
input[type='text']:focus {
  outline: none;
  border-color: #1a237e;
}

button {
  background: #1a237e;
  color: white;
  border: none;
  padding: 0.75rem 1.5rem;
  font-size: 1rem;
  border-radius: 6px;
  cursor: pointer;
  transition: background 0.2s;
}

button:hover:not(:disabled) {
  background: #283593;
}

button:disabled {
  background: #9fa8da;
  cursor: not-allowed;
}

.response {
  margin-top: 1.5rem;
  background: white;
  border: 1px solid #ddd;
  border-radius: 6px;
  padding: 1rem 1.25rem;
  text-align: left;
}

.response h1,
.response h2,
.response h3 {
  line-height: 1.3;
  margin-bottom: 0.5rem;
}

.response h2 {
  font-size: 1rem;
  color: #1a237e;
}

.response h1,
.response h3 {
  margin-top: 0.75rem;
  color: #1a237e;
}

.response p {
  margin-bottom: 0.75rem;
}

.response ul,
.response ol {
  margin: 0 0 0.75rem 1.25rem;
}

.response li {
  margin-bottom: 0.25rem;
}

.error {
  margin-top: 1rem;
  color: #c62828;
  background: #ffebee;
  border-radius: 4px;
  padding: 0.75rem;
}

.success {
  margin-top: 1rem;
  color: #2e7d32;
  background: #e8f5e9;
  border-radius: 4px;
  padding: 0.75rem;
}
</style>

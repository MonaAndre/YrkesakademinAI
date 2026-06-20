<template>
  <div id="app">
    <header>
      <div class="logo">
        <span class="logo-icon">🎓</span>
        <div class="logo-text">
          <span class="logo-title">Yrkesakademin<span class="logo-ai">AI</span></span>
        </div>
      </div>
      <nav>
        <RouterLink to="/feedback">Feedback</RouterLink>
        <RouterLink to="/kursmaterial">Kursmaterial</RouterLink>
        <RouterLink to="/faq">FAQ</RouterLink>
      </nav>
    </header>
    <main>
      <RouterView />
    </main>
    <footer>
      <span>© 2026 YrkesakademinAI</span>
      <span class="footer-divider">·</span>
      <span>Drivs av Claude AI</span>
    </footer>
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
  background: linear-gradient(135deg, #1a237e 0%, #283593 100%);
  color: white;
  padding: 1rem 2rem;
  display: flex;
  align-items: center;
  justify-content: space-between;
  box-shadow: 0 2px 8px rgba(0, 0, 0, 0.25);
}

.logo {
  display: flex;
  align-items: center;
  gap: 0.75rem;
}

.logo-icon {
  font-size: 2rem;
  line-height: 1;
}

.logo-text {
  display: flex;
  flex-direction: column;
  line-height: 1.2;
}

.logo-title {
  font-size: 1.25rem;
  font-weight: 700;
  letter-spacing: 0.01em;
}

.logo-ai {
  display: inline-block;
  background: #ffd740;
  color: #1a237e;
  font-size: 0.85rem;
  font-weight: 800;
  padding: 0.05em 0.3em;
  border-radius: 4px;
  margin-left: 0.2em;
  vertical-align: middle;
  letter-spacing: 0.05em;
}

.logo-sub {
  font-size: 0.72rem;
  opacity: 0.75;
  letter-spacing: 0.04em;
  text-transform: uppercase;
}

footer {
  background: #1a237e;
  color: rgba(255, 255, 255, 0.7);
  font-size: 0.8rem;
  padding: 1rem 2rem;
  display: flex;
  justify-content: center;
  gap: 0.5rem;
  flex-wrap: wrap;
}

.footer-divider {
  opacity: 0.4;
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

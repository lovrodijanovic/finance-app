const PROXY_CONFIG = [
  {
    context: [
      "/financialForm",
    ],
    target: "https://localhost:7008",
    secure: false
  }
]

module.exports = PROXY_CONFIG;

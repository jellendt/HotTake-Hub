module.exports = {
  branches: ["main"],
  plugins: [
    "@semantic-release/commit-analyzer",
    "@semantic-release/release-notes-generator",
    "@semantic-release/changelog",
    [
      "@semantic-release/git",
      {
        assets: [
          "packages/frontend/package.json",
          "packages/backend/**/*.csproj",
          "CHANGELOG.md",
        ],
        message: "chore(release): ${nextRelease.version}",
      },
    ],
    "@semantic-release/github",
  ],
};

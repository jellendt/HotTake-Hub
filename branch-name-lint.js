const branch = require("child_process")
  .execSync("git rev-parse --abbrev-ref HEAD")
  .toString()
  .trim();

const pattern =
  /^(feature|hotfix|chore|refactor|docs|test|style|perf)\/[a-z0-9-]+$/;

if (!pattern.test(branch)) {
  console.error(`
❌ Invalid branch name: ${branch}

Allowed format:

type/name

Examples:
feature/login-page
hotfix/auth-token
chore/update-deps
`);

  process.exit(1);
}

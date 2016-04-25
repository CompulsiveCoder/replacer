echo "Testing replacer project from github"
echo "  Current directory:"
echo "  $PWD"

BRANCH=$1

if [ -z "$BRANCH" ]; then
    BRANCH=$(git branch | sed -n -e 's/^\* \(.*\)/\1/p')
fi

if [ -z "$BRANCH" ]; then
    BRANCH="master"
fi

echo "  Branch: $BRANCH"

# If the .tmp/replacer directory exists then remove it
if [ -d ".tmp/replacer" ]; then
    rm .tmp/replacer -rf
fi

DIR=$PWD

git clone https://github.com/CompulsiveCoder/replacer.git .tmp/replacer --branch $BRANCH
cd .tmp/replacer && \
sh init-build-test.sh && \
cd $DIR && \
rm .tmp/replacer -rf

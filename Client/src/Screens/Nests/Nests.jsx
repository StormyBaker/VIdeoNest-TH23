import { useEffect } from "react";
import { useState } from "react";
import { NestCard } from "../../Components/NestCard/NestCard";
import { NestCardFooter } from "../../Components/NestCardFooter/NestCardFooter";
import { getAllNests } from "../../Service/NestService";
import { Card } from "primereact/card";

export function Nests() {
  const [nests, setNests] = useState([]);

  useEffect(() => {
    (async () => {
      const response = await getAllNests();

      if (response.status === 200) {
        setNests(response.data.playlists);
      }
    })();
  }, []);

  return (
    <div>
      <div className="grid gap-2">
        {nests.map((nest) => {
          return (
            <NestCard
              key={nest.guid}
              title={nest.name}
              subtitle={nest.description}
              footer={<NestCardFooter guid={nest.guid} />}
            />
          );
        })}

        <Card
          className="col-12 md:col-3"
          title="Build a new Nest"
          subTitle="Future feature - create as many nests to store your treasures as you like."
        />
      </div>
    </div>
  );
}
